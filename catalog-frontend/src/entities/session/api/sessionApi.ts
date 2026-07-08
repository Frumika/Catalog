import {apiClient, ApiError} from "@/shared/api";
import type {Session} from "../model/types.ts";


const ENDPOINT = "api/auth";

export const sessionApi = {
    getSession: async (sessionId: string): Promise<Session> => {
        const response = await apiClient.get<Session>(
            `${ENDPOINT}/${sessionId}`,
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    sendCode: async (email: string): Promise<void> => {
        const response = await apiClient.post<void>(
            `${ENDPOINT}/send_code`,
            {email},
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },

    verify: async (email: string, code: string): Promise<Session> => {
        const response = await apiClient.post<Session>(
            `${ENDPOINT}/verify`,
            {email, code},
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    logout: async (sessionId: string): Promise<void> => {
        const response = await apiClient.delete<void>(
            `${ENDPOINT}/logout`,
            {sessionId},
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },

    logoutAll: async (sessionId: string): Promise<void> => {
        const response = await apiClient.delete<void>(
            `${ENDPOINT}/logout_all`,
            {sessionId},
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },
}