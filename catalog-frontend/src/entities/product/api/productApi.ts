import {apiClient, ApiError} from "@/shared/api";
import type {ProductDetails, ProductPreview} from "../model/Product.types.ts";

const ENDPOINT = "api/catalog";

export const productApi = {

    products: async (): Promise<ProductPreview[]> => {
        let response = await apiClient.post<ProductPreview[]>(
            `${ENDPOINT}/product/list`,
            {
                pageNumber: 1,
                pageSize: 5,
            }
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },

    getById: async (id: number): Promise<ProductDetails> => {
        let response = await apiClient.get<ProductDetails>(
            `${ENDPOINT}/product/${id}`
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },
}