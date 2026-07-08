import {apiClient, ApiError} from "@/shared/api";
import type {CartPosition} from "../model/types.ts";
import {mapCartPosition} from "./mappers.ts";


const ENDPOINT = "api/cart";

interface CartResult<T> {
    items: T[];
    totalQuantity: number;
    totalBasePrice: number,
    totalDiscountAmount: number,
    finalPrice: number;
}

export const cartApi = {
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
}