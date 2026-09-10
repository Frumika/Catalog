import {apiClient, ApiError} from "@/shared/api";
import type {ExtendedOrder, Order} from "../model/types.ts";
import type {ExtendedOrderDto, OrderDto} from "@/entities/order/api/dto.ts";
import {mapExtendedOrder, mapOrder} from "@/entities/order/api/mappers.ts";


const ENDPOINT = "api/order"

export const orderApi = {

    getById: async (orderId: number): Promise<ExtendedOrder> => {
        const response = await apiClient.get<ExtendedOrderDto>(`${ENDPOINT}/${orderId}`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return mapExtendedOrder(response.data);
    },

    makeOrder: async (productIds: number[], pickupPointId: number): Promise<ExtendedOrder> => {
        const response = await apiClient.post<ExtendedOrderDto>(
            `${ENDPOINT}/make/`,
            {productIds, pickupPointId},
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return mapExtendedOrder(response.data);
    },

    payOrder: async (orderId: number): Promise<Order> => {
        const response = await apiClient.post<OrderDto>(`${ENDPOINT}/pay/${orderId}`,)

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return mapOrder(response.data);
    },

    cancelOrder: async (orderId: number): Promise<void> => {
        const response = await apiClient.delete<Order>(`${ENDPOINT}/cancel/${orderId}`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },

    getPendingOrders: async (): Promise<Order[]> => {
        const response = await apiClient.get<OrderDto[]>(`${ENDPOINT}`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data.map(dto => mapOrder(dto));
    },
}