import {useCallback} from "react";
import {productApi} from "../api/productApi.ts";
import {usePaginatedList} from "@/shared/lib/usePaginatedList.ts";


const PAGE_SIZE = 8;

export const useProductList = () => {
    const fetchPage = useCallback(
        (page: number, pageSize: number) => productApi.products(page, pageSize),
        []
    );

    return usePaginatedList(fetchPage, PAGE_SIZE);
};