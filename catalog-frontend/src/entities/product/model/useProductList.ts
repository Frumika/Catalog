import {useCallback, useEffect, useRef, useState} from "react";
import {productApi} from "../api/productApi.ts";
import type {ProductPreview} from "./Product.types.ts";
import {ApiError, toApiError} from "@/shared/api";


const PAGE_SIZE = 8;

interface UseProductListResult {
    products: ProductPreview[];
    isLoading: boolean;
    error: ApiError | null;
    hasMore: boolean;
    loadMore: () => void;
}

export const useProductList = (): UseProductListResult => {
    const [products, setProducts] = useState<ProductPreview[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<ApiError | null>(null);
    const [hasMore, setHasMore] = useState(true);

    const pageRef = useRef(1);
    const isFetchingRef = useRef(false);

    const fetchPage = useCallback(async (page: number) => {
        if (isFetchingRef.current) return;
        isFetchingRef.current = true;

        setIsLoading(true);
        setError(null);

        try {
            const result = await productApi.products(page, PAGE_SIZE);
            setProducts(prev => [...prev, ...result.items]);

            const loadedCount = page * PAGE_SIZE;
            setHasMore(loadedCount < result.totalCount);

            pageRef.current = page;
        } catch (err) {
            setError(toApiError(err));
        } finally {
            setIsLoading(false);
            isFetchingRef.current = false;
        }
    }, []);

    useEffect(() => {
        void fetchPage(1);
    }, [fetchPage]);

    const loadMore = useCallback(() => {
        if (!hasMore) return;
        void fetchPage(pageRef.current + 1);
    }, [hasMore, fetchPage]);

    return {products, isLoading, error, hasMore, loadMore};
};