import {useCallback, useEffect, useRef, useState} from "react";
import {productApi} from "../api/productApi.ts";
import type {ProductPreview} from "./Product.types.ts";
import {ApiError, toApiError} from "@/shared/api";


const PAGE_SIZE = 20;

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
            const data = await productApi.products(page, PAGE_SIZE);
            setProducts(prev => [...prev, ...data]);
            setHasMore(data.length === PAGE_SIZE);
            pageRef.current = page;
        } catch (error) {
            setError(toApiError(error));
        } finally {
            setIsLoading(false);
            isFetchingRef.current = false;
        }
    }, []);

    useEffect(() => {
        fetchPage(1);
    }, [fetchPage]);

    const loadMore = useCallback(() => {
        if (!hasMore) return;
        fetchPage(pageRef.current + 1);
    }, [hasMore, fetchPage]);

    return {products, isLoading, error, hasMore, loadMore};
};