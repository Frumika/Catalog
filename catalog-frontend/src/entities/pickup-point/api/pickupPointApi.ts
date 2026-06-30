import type {PickupPoint} from "@/entities/pickup-point/model/PickupPoint.types.ts";
import {apiClient, ApiError} from "@/shared/api";


const ENDPOINT = 'api/pickup_point';

export const pickupPointApi = {
    getAll: async (): Promise<PickupPoint[]> => {
        let response = await apiClient.get<PickupPoint[]>(
            `${ENDPOINT}/all`,
            true
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    select: async (id: string): Promise<PickupPoint> => {
        let response = await apiClient.patch<PickupPoint>(
            `${ENDPOINT}/select/`,
            {pickupPointId: id},
            true
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    delete: async (id: string): Promise<void> => {
        let response = await apiClient.delete<void>(`${ENDPOINT}/remove/${id}`,);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },

    add: async (address: string): Promise<PickupPoint> => {
        let response = await apiClient.post<PickupPoint>(ENDPOINT, {address});

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    }
}