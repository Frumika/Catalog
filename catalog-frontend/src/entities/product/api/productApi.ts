import {apiClient, ApiError, getFullUrl} from "@/shared/api";
import type {ProductDetails, ProductPreview} from "../model/Product.types.ts";
import {mapProductPreview} from "./mappers.ts";

const ENDPOINT = "api/catalog";

export const productApi = {
    products: async (pageNumber: number, pageSize: number): Promise<ProductPreview[]> => {
        let response = await apiClient.post<ProductPreview[]>(
            `${ENDPOINT}/product/list`,
            {
                pageNumber: pageNumber,
                pageSize: pageSize,
            }
        );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data.map(mapProductPreview);
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