import {apiClient, ApiError} from "@/shared/api";
import type {User} from "@/entities/user";

const ENDPOINT = "api/auth";

export const userApi = {
    getUser: async (): Promise<User> => {
        const response = await apiClient.post<User>(
            `${ENDPOINT}/user`,
            {},
            true
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },
}