import {useCallback} from "react";
import {productApi} from "../api/productApi.ts";
import {usePaginatedList} from "@/shared/lib/usePaginatedList.ts";
import type {ProductFilters} from "@/entities/product";


const PAGE_SIZE = 8;

export const useProductList = (filters?: ProductFilters) => {
    const fetchPage = useCallback(
        (page: number, pageSize: number) =>
            productApi.products(page, pageSize, filters),
        [filters]
    );

    return usePaginatedList(fetchPage, PAGE_SIZE);
};