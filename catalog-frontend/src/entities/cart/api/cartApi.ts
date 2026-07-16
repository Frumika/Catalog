import {apiClient, ApiError} from "@/shared/api";
import type {CartPosition, CartPositionPreview} from "../model/types.ts";
import {mapCartPosition} from "./mappers.ts";
import type {CartPositionDto, CartPositionPreviewDto, CartResponse} from "./dto.ts";


const ENDPOINT = "api/cart";

export const cartApi = {

    getCartPreview: async (): Promise<CartResponse<CartPositionPreview>> => {
        const response = await apiClient
            .post<CartResponse<CartPositionPreviewDto>>(`${ENDPOINT}/preview`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return {
            ...response.data,
            items: response.data.items,
        };
    },


    addProduct: async (productId: number): Promise<CartPositionPreview> => {
        const response = await apiClient
            .post<CartPositionPreviewDto>(`${ENDPOINT}/product/add`, {productId});

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },


    updateQuantity: async (productId: number, quantity: number): Promise<CartPositionPreview> => {
        const response = await apiClient
            .patch<CartPositionPreviewDto>(`${ENDPOINT}/product/quantity/update`, {productId, quantity});

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },


    removeItem: async (productId: number): Promise<CartPositionPreview> => {
        const response = await apiClient
            .delete<CartPositionPreviewDto>(`${ENDPOINT}/product/remove`, {productId});

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },


    clearCart: async () => {
        let response = await apiClient.delete(`${ENDPOINT}/clear`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },


    getCartPositions: async (): Promise<CartResponse<CartPosition>> => {
        let response = await apiClient
            .post<CartResponse<CartPositionDto>>(`${ENDPOINT}/get`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return {
            ...response.data,
            items: response.data.items.map(mapCartPosition),
        };
    },
};