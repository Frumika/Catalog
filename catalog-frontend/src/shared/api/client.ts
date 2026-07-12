import {tokenLocalStorage} from "./tokenLocalStorage.ts";
import type {ApiResponse, RefreshResponse, RequestBody} from "./types.ts";


const BASE_URL = "http://localhost:8000/";

let refreshPromise: Promise<ApiResponse<RefreshResponse> | null> | null = null;

async function request<TData>(
    url: string,
    method: string,
    body: RequestBody = {},
    authorization = true,
    headers?: HeadersInit
): Promise<ApiResponse<TData>> {

    let fullUrl = getFullUrl(url);
    const resultBody: RequestBody = {...body};

    let fetchHeaders: Record<string, string> = {
        'Content-Type': 'application/json',
        ...(headers as Record<string, string>),
    };

    if (authorization) {
        const accessToken = tokenLocalStorage.getAccessToken();
        if (accessToken) {
            fetchHeaders['Authorization'] = `Bearer ${accessToken}`;
        }
    }

    let response = await fetch(fullUrl, {
        method,
        headers: fetchHeaders,
        ...(method !== 'GET' && {body: JSON.stringify(resultBody)}),
    });

    if (response.status === 401 && authorization) {
        const refreshToken = tokenLocalStorage.getRefreshToken();
        if (refreshToken) {

            if (!refreshPromise) {
                refreshPromise = fetch(getFullUrl('api/session/refresh'), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({refreshToken}),
                })
                    .then(async (res) => {
                        if (res.ok) {
                            const response = await res.json() as ApiResponse<RefreshResponse>;

                            tokenLocalStorage.setAccessToken(response.data.accessToken);
                            if (response.data.refreshToken) {
                                tokenLocalStorage.setRefreshToken(response.data.refreshToken);
                            }
                            return response;
                        }
                        if (res.status === 401 || res.status === 403) {
                            tokenLocalStorage.clearStorage();
                        }
                        return null;
                    })
                    .catch(() => {
                        return null;
                    })
                    .finally(() => {
                        refreshPromise = null;
                    });
            }

            const refreshResult = await refreshPromise;

            if (refreshResult) {
                fetchHeaders['Authorization'] = `Bearer ${refreshResult.data.accessToken}`;

                response = await fetch(fullUrl, {
                    method,
                    headers: fetchHeaders,
                    ...(method !== 'GET' && {body: JSON.stringify(resultBody)}),
                });
            }
        } else {
            tokenLocalStorage.clearStorage();
        }
    }

    const result = await response.json() as Omit<ApiResponse<TData>, 'ok'>;

    return {...result, ok: response.ok};
}

export const apiClient = {
    get<TData>(url: string, authorization = false, headers?: HeadersInit) {
        return request<TData>(url, 'GET', {}, authorization, headers);
    },

    post<TData>(url: string, body: RequestBody = {}, authorization = false, headers?: HeadersInit) {
        return request<TData>(url, 'POST', body, authorization, headers);
    },

    put<TData>(url: string, body: RequestBody = {}, authorization = false, headers?: HeadersInit) {
        return request<TData>(url, 'PUT', body, authorization, headers);
    },

    patch<TData>(url: string, body: RequestBody = {}, authorization = false, headers?: HeadersInit) {
        return request<TData>(url, 'PATCH', body, authorization, headers);
    },

    delete<TData>(url: string, body: RequestBody = {}, authorization = false, headers?: HeadersInit) {
        return request<TData>(url, 'DELETE', body, authorization, headers);
    },
};

export const getFullUrl = (path: string): string => new URL(path, BASE_URL).toString();
