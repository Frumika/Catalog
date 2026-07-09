// entities/cart-item/api/cartApi.ts
import { apiClient, ApiError } from "@/shared/api";
import type { CartPosition } from "../model/types.ts";
import { mapCartPosition } from "./mappers.ts";

const ENDPOINT = "api/cart";

interface CartResult<T> {
    items: T[];
    totalQuantity: number;
    totalBasePrice: number;
    totalDiscountAmount: number;
    finalPrice: number;
}

export const cartApi = {
    // 1. Получение корзины (как и было)
    getCartPositions: async (): Promise<CartResult<CartPosition>> => {
        let response = await apiClient.post<CartResult<CartPosition>>(
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
        // Если твой apiClient не поддерживает метод patch напрямую, убедись, что внутри него 
        // обернут стандартный fetch с методом 'PATCH' или Axios.patch
        let response = await apiClient.patch(
            `${ENDPOINT}/product/quantity/update`,
            { productId, quantity },
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
            { productId }, // передаем в body, либо если бэкенд ждет в URL, перепиши на `${ENDPOINT}/product/remove?productId=${productId}`
            true
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },
    addProductToCart: async (productId: number, quantity: number = 1): Promise<void> => {
        let response = await apiClient.post(
            `${ENDPOINT}/product/add`,
            { productId, quantity },
            true
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }
    },
};