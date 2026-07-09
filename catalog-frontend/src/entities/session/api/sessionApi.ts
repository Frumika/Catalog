import {apiClient, ApiError} from "@/shared/api";
import type {Session} from "../model/types.ts";


const ENDPOINT = "api/auth";

export const sessionApi = {
    sendCode: async (email: string): Promise<void> => {
        const response = await apiClient.post<void>(
            `${ENDPOINT}/send_code`,
            {email},
            false,
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },

    verify: async (email: string, code: string): Promise<Session> => {
        const response = await apiClient.post<Session>(
            `${ENDPOINT}/verify`,
            {email, code},
            false,
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    logout: async (refreshToken: string): Promise<void> => {
        const response = await apiClient.delete<void>(
            `${ENDPOINT}/logout`,
            {refreshToken},
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },

    logoutAll: async (): Promise<void> => {
        const response = await apiClient.delete<void>(
            `${ENDPOINT}/logout_all`,
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },
}