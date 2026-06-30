import type {PickupPoint} from "@/entities/pickup-point/model/PickupPoint.types.ts";
import {apiClient} from "@/shared/api";


const ENDPOINT = 'api/pickup_point';

export const pickupPointApi = {
    getAll: async (): Promise<PickupPoint[]> => {
        let response = await apiClient.get<PickupPoint[]>(
            `${ENDPOINT}/all`,
            true
        );

        return response.data as PickupPoint[];
    },

    select: async (id: string): Promise<PickupPoint> => {
        let response = await apiClient.patch<PickupPoint>(
            `${ENDPOINT}/select/${id}`,
            {id},
            true
        );

        return response.data as PickupPoint;
    },

    delete: async (id: string): Promise<void> => {
        let response = await apiClient.delete<void>(`${ENDPOINT}/remove/${id}`,)
    },

    add: async (address: string): Promise<PickupPoint> => {
        let response = await apiClient.post<PickupPoint>(ENDPOINT, {address})
        return response.data as PickupPoint;
    }
}