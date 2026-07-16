import {apiClient, ApiError, type PagedResult} from "@/shared/api";
import type {ProductDetails, ProductFilters, ProductPreview} from "../model/types.ts";
import {mapProductPreview} from "./mappers.ts";


const ENDPOINT = "api/catalog";

export const productApi = {
    products: async (pageNumber: number, pageSize: number, filters?: ProductFilters): Promise<PagedResult<ProductPreview>> => {
        let response = await apiClient
            .post<PagedResult<ProductPreview>>(
                `${ENDPOINT}/product/list`,
                {
                    pageNumber,
                    pageSize,
                    ...filters,
                }
            );

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return {
            ...response.data,
            items: response.data.items.map(mapProductPreview),
        };
    },

    getById: async (id: number): Promise<ProductDetails> => {
        let response = await apiClient
            .get<ProductDetails>(`${ENDPOINT}/product/${id}`);

        if (!response.ok) {
            throw new ApiError(response.code, response.message);
        }

        return response.data;
    },
}