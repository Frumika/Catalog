import {session} from "@/shared/api/session.ts";

type RequestBody = Record<string, unknown>;


const BASE_URL = "http://localhost:8000/";

async function request<T>(
    url: string,
    method: string,
    body: RequestBody = {},
    authorization = false,
    headers?: HeadersInit
): Promise<T> {
    const resultBody: RequestBody = {...body};

    if (authorization) {
        const sessionId = session.get();
        if (sessionId) {
            resultBody.UserSessionId = sessionId;
        }
    }

    const response = await fetch(
        BASE_URL + url,
        {
            method,
            headers: {
                'Content-Type': 'application/json',
                ...headers,
            },
            body: JSON.stringify(resultBody),
        }
    );

    if (!response.ok) {
        throw new Error(`${method} ${url} — ${response.status}`);
    }

    const finalResponse: T = await response.json() as T;

    console.log(`Raw response: ${response}`);
    console.log(`Response body: ${finalResponse}`);

    return finalResponse;
}

export const apiClient = {
    get<T>(url: string, authorization = false, headers?: HeadersInit): Promise<T> {
        return request<T>(url, 'GET', {}, authorization, headers);
    },
    post<T>(url: string, body: RequestBody = {}, authorization = false, headers?: HeadersInit): Promise<T> {
        return request<T>(url, 'POST', body, authorization, headers);
    },

    put<T>(url: string, body: RequestBody = {}, authorization = false, headers?: HeadersInit): Promise<T> {
        return request<T>(url, 'PUT', body, authorization, headers);
    },

    patch<T>(url: string, body: RequestBody = {}, authorization = false, headers?: HeadersInit): Promise<T> {
        return request<T>(url, 'PATCH', body, authorization, headers);
    },

    delete<T>(url: string, body: RequestBody = {}, authorization = false, headers?: HeadersInit): Promise<T> {
        return request<T>(url, 'DELETE', body, authorization, headers);
    },
};