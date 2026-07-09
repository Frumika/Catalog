import {apiClient, ApiError} from "@/shared/api";
import type {CartPosition, CartPositionPreview} from "../model/types.ts";
import {mapCartPosition} from "./mappers.ts";
import type {CartPositionDto, CartPositionPreviewDto} from "@/entities/cart/api/Cart.dto.ts";


const ENDPOINT = "api/cart";

interface CartPreviewResult<T> {
    items: T[];
    totalQuantity: number
}

interface CartResult<T> {
    items: T[];
    totalQuantity: number;
    totalBasePrice: number;
    totalDiscountAmount: number;
    finalPrice: number;
}

export const cartApi = {

    getCartPreview: async (): Promise<CartPreviewResult<CartPositionPreview>> => {
        let response = await apiClient.post<CartPreviewResult<CartPositionPreviewDto>>(
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

    getCartPositions: async (): Promise<CartResult<CartPosition>> => {
        let response = await apiClient.post<CartResult<CartPositionDto>>(
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

    // 2. PATCH-запрос на обновление количества
    updateQuantity: async (productId: number, quantity: number): Promise<void> => {
        let response = await apiClient.patch(
            `${ENDPOINT}/product/quantity/update`,
            {productId, quantity},
            true
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },

    // 3. DELETE-запрос на удаление конкретного продукта
    removeItem: async (productId: number): Promise<void> => {
        let response = await apiClient.delete(
            `${ENDPOINT}/product/remove`,
            {productId},
            true
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },
    addProductToCart: async (productId: number, quantity: number = 1): Promise<void> => {
        let response = await apiClient.post(
            `${ENDPOINT}/product/add`,
            {productId, quantity},
            true
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },
};