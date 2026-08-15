import type {PickupPoint} from "@/entities/pickup-point/model/types.ts";
import {apiClient, ApiError} from "@/shared/api";


const ENDPOINT = 'api/pickup_point';

export const pickupPointApi = {
    getAll: async (): Promise<PickupPoint[]> => {
        let response = await apiClient
            .get<PickupPoint[]>(`${ENDPOINT}/all`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    select: async (id: number): Promise<PickupPoint> => {
        let response = await apiClient
            .patch<PickupPoint>(`${ENDPOINT}/select/`, {pickupPointId: id});

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    remove: async (id: number): Promise<void> => {
        let response = await apiClient
            .delete<void>(`${ENDPOINT}/remove/${id}`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },

    add: async (address: string): Promise<PickupPoint> => {
        let response = await apiClient
            .post<PickupPoint>(ENDPOINT, {address});

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    }
}