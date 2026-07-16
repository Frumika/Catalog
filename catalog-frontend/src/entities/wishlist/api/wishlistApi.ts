import {apiClient, ApiError} from "@/shared/api";
import type {WishedProduct, Wishlist} from "@/entities/wishlist/model/types.ts";


const ENDPOINT = "api/wishlist"

export const wishlistApi = {

    preview: async (): Promise<Wishlist> => {
        const response = await apiClient
            .post<Wishlist>(`${ENDPOINT}/preview`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    addProduct: async (productId: number): Promise<WishedProduct> => {
        const response = await apiClient
            .post<WishedProduct>(`${ENDPOINT}/product/add`, {productId});

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    removeProduct: async (productId: number): Promise<WishedProduct> => {
        const response = await apiClient
            .delete<WishedProduct>(`${ENDPOINT}/product/remove`, {productId});

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },
}