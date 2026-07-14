import {apiClient, ApiError} from "@/shared/api";
import type {CartPosition, CartPositionPreview} from "../model/types.ts";
import {mapCartPosition} from "./mappers.ts";
import type {CartPositionDto, CartPositionPreviewDto, CartResponse} from "./dto.ts";


const ENDPOINT = "api/cart";

export const cartApi = {

    // Получение минимального набора данных о корзине
    getCartPreview: async (): Promise<CartResponse<CartPositionPreview>> => {
        const response = await apiClient
            .post<CartResponse<CartPositionPreviewDto>>
            (
                `${ENDPOINT}/preview`,
                {},
                true
            );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return {
            ...response.data,
            items: response.data.items,
        };
    },

    // Добавление нового товара в корзину
    addProduct: async (productId: number): Promise<CartPositionPreview> => {
        const response = await apiClient
            .post<CartPositionPreviewDto>(
                `${ENDPOINT}/product/add`,
                {productId},
                true
            );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },


    // Обновление количество уже существующего товара в корзине
    updateQuantity: async (productId: number, quantity: number): Promise<CartPositionPreview> => {
        const response = await apiClient
            .patch<CartPositionPreviewDto>(
                `${ENDPOINT}/product/quantity/update`,
                {productId, quantity},
                true
            );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    // Удаление товара из корзины
    removeItem: async (productId: number): Promise<CartPositionPreview> => {
        const response = await apiClient
            .delete<CartPositionPreviewDto>(
                `${ENDPOINT}/product/remove`,
                {productId},
                true
            );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    // Очистка всей корзины
    clearCart: async () => {
        let response = await apiClient
            .delete(
                `${ENDPOINT}/clear`,
                {},
                true
            );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },

    // Получение полной информации о корзине и товарах в ней
    getCartPositions: async (): Promise<CartResponse<CartPosition>> => {
        let response = await apiClient.post<CartResponse<CartPositionDto>>(
            `${ENDPOINT}/get`,
            {},
            true
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return {
            ...response.data,
            items: response.data.items.map(mapCartPosition),
        };
    },
};