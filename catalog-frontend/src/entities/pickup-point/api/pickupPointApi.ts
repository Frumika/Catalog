import type {PickupPoint} from "@/entities/pickup-point/model/PickupPoint.types.ts";
import {apiClient} from "@/shared/api";


const ENDPOINT = 'api/pickup-point';

export const PickupPointApi = {

    getAll: (): Promise<PickupPoint[]> =>
        apiClient.get<PickupPoint[]>(`${ENDPOINT}/all`),

    select: (id: string): Promise<PickupPoint> =>
        apiClient.patch<PickupPoint>(`${ENDPOINT}/select/${id}`),

    delete: (id: string): Promise<void> =>
        apiClient.delete<void>(`${ENDPOINT}/remove/${id}`),

    add: (address: string): Promise<PickupPoint> =>
        apiClient.post<PickupPoint>(ENDPOINT, {address}),
}