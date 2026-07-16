import {useCallback, useMemo} from "react";
import {productApi} from "../api/productApi.ts";
import {usePaginatedList} from "@/shared/lib/usePaginatedList.ts";
import type {ProductFilters} from "@/entities/product";


const PAGE_SIZE = 8;

export const useProducts = (filters?: ProductFilters) => {

    const filtersKey = JSON.stringify(filters);
    const stableFilters = useMemo(() => filters, [filtersKey]);

    const fetchPage = useCallback(
        (page: number, pageSize: number) =>
            productApi.products(page, pageSize, stableFilters),
        [stableFilters]
    );

    return usePaginatedList(fetchPage, PAGE_SIZE);
};