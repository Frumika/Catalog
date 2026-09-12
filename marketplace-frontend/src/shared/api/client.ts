import {tokenLocalStorage} from "./tokenLocalStorage.ts";
import type {ApiResponse, RefreshResponse, RequestBody} from "./types.ts";


const BASE_URL = "http://localhost:8000/";
export const getFullUrl = (path: string): string => new URL(path, BASE_URL).toString();

const GATEWAY_STATUSES = new Set([502, 503, 504]);
const GATEWAY_RETRY_COUNT = 2;
const GATEWAY_RETRY_DELAY_MS = 300;

let refreshPromise: Promise<boolean> | null = null;

const sleep = (ms: number) => new Promise<void>((resolve) => setTimeout(resolve, ms));

async function fetchWithGatewayRetry(url: string, init: RequestInit): Promise<Response> {
    let response: Response;
    for (let attempt = 0; ; attempt++) {
        response = await fetch(url, init);
        if (!GATEWAY_STATUSES.has(response.status) || attempt >= GATEWAY_RETRY_COUNT) {
            return response;
        }
        await sleep(GATEWAY_RETRY_DELAY_MS * (attempt + 1));
    }
}

async function safeParseJson<T>(response: Response): Promise<T | null> {
    const rawText = await response.text();
    if (!rawText) return null;
    try {
        return JSON.parse(rawText) as T;
    } catch {
        return null;
    }
}

function toGatewayErrorResponse<TData>(response: Response): ApiResponse<TData> {
    return {
        ok: false,
        code: String(response.status),
        message: "Сервис временно недоступен, попробуйте обновить страницу",
        data: undefined,
    } as ApiResponse<TData>;
}

async function attemptRefreshSession(): Promise<boolean> {
    const refreshToken = tokenLocalStorage.getRefreshToken();
    if (!refreshToken) return false;

    if (!refreshPromise) {
        refreshPromise = fetchWithGatewayRetry(getFullUrl('api/session/refresh'), {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({refreshToken}),
        })
            .then(async (res) => {
                if (res.ok) {
                    const parsed = await safeParseJson<ApiResponse<RefreshResponse>>(res);
                    if (parsed?.data) {
                        tokenLocalStorage.setAccessToken(parsed.data.accessToken);
                        if (parsed.data.refreshToken) {
                            tokenLocalStorage.setRefreshToken(parsed.data.refreshToken);
                        }
                        return true;
                    }
                }

                if (res.status === 401 || res.status === 403 || res.status === 404) {
                    tokenLocalStorage.clearStorage();
                }
                return false;
            })
            .catch(() => false)
            .finally(() => {
                refreshPromise = null;
            });
    }

    return await refreshPromise;
}

function createRequestInit(method: string, body: RequestBody | undefined, headers?: HeadersInit): RequestInit {
    const fetchHeaders: Record<string, string> = {
        'Content-Type': 'application/json',
        ...(headers as Record<string, string>),
    };

    const accessToken = tokenLocalStorage.getAccessToken();
    if (accessToken) {
        fetchHeaders['Authorization'] = `Bearer ${accessToken}`;
    }

    return {
        method,
        headers: fetchHeaders,
        ...(method !== 'GET' && body !== undefined && {body: JSON.stringify(body)}),
    };
}

async function request<TData>(
    url: string,
    method: string,
    body: RequestBody | undefined,
    authorization = true,
    headers?: HeadersInit
): Promise<ApiResponse<TData>> {

    const fullUrl = getFullUrl(url);

    let response = await fetchWithGatewayRetry(
        fullUrl,
        createRequestInit(method, body, headers)
    );

    if (response.status === 401 && authorization) {
        const isRefreshed = await attemptRefreshSession();

        if (isRefreshed) {
            response = await fetchWithGatewayRetry(
                fullUrl,
                createRequestInit(method, body, headers)
            );
        } else {
            tokenLocalStorage.clearStorage();
        }
    }

    const parsedResult = await safeParseJson<Omit<ApiResponse<TData>, 'ok'>>(response);

    if (!parsedResult) {
        return toGatewayErrorResponse<TData>(response);
    }

    return {...parsedResult, ok: response.ok};
}

export const apiClient = {
    get<TData>(url: string, authorization = true, headers?: HeadersInit) {
        return request<TData>(url, 'GET', undefined, authorization, headers);
    },

    post<TData>(url: string, body: RequestBody | undefined = undefined, authorization = true, headers?: HeadersInit) {
        return request<TData>(url, 'POST', body, authorization, headers);
    },

    put<TData>(url: string, body: RequestBody | undefined = undefined, authorization = true, headers?: HeadersInit) {
        return request<TData>(url, 'PUT', body, authorization, headers);
    },

    patch<TData>(url: string, body: RequestBody | undefined = undefined, authorization = true, headers?: HeadersInit) {
        return request<TData>(url, 'PATCH', body, authorization, headers);
    },

    delete<TData>(url: string, body: RequestBody | undefined = undefined, authorization = true, headers?: HeadersInit) {
        return request<TData>(url, 'DELETE', body, authorization, headers);
    },
};
