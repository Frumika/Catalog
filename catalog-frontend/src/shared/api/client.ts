import {session} from "./session.ts";
import type {ApiResponse, RequestBody} from "./types.ts";


const BASE_URL = "http://localhost:8000/";

async function request<TData>(
    url: string,
    method: string,
    body: RequestBody = {},
    authorization = false,
    headers?: HeadersInit
): Promise<ApiResponse<TData>> {

    let fullUrl = getFullUrl(url);
    const resultBody: RequestBody = {...body};

    if (authorization) {
        const sessionId = session.get();
        if (sessionId) {
            if (method === 'GET') {
                fullUrl += `/${sessionId}`;
            } else {
                resultBody.UserSessionId = sessionId;
            }
        }
    }

    const response = await fetch(fullUrl, {
        method,
        headers: {
            'Content-Type': 'application/json',
            ...headers,
        },
        ...(method !== 'GET' && {body: JSON.stringify(resultBody)}),
    });

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
