import {useCallback, useEffect, useRef, useState} from 'react';
import {ApiError, toApiError, type PagedResult} from '../api';


type FetchPage<T> = (page: number, pageSize: number) => Promise<PagedResult<T>>;

interface UsePaginatedListResult<T> {
    items: T[];
    isLoading: boolean;
    error: ApiError | null;
    hasMore: boolean;
    loadMore: () => void;
}

export const usePaginatedList = <T>(
    fetchPage: FetchPage<T>,
    pageSize = 8
): UsePaginatedListResult<T> => {
    const [items, setItems] = useState<T[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<ApiError | null>(null);
    const [hasMore, setHasMore] = useState(true);

    const pageRef = useRef(1);
    const requestIdRef = useRef(0);
    const isFetchingRef = useRef(false);

    const load = useCallback(async (page: number, isFirst: boolean) => {
        if (isFetchingRef.current) return;
        isFetchingRef.current = true;

        const requestId = ++requestIdRef.current;

        setIsLoading(true);
        setError(null);

        try {
            const result = await fetchPage(page, pageSize);

            if (requestId !== requestIdRef.current) return;

            setItems(prev => isFirst ? result.items : [...prev, ...result.items]);
            const loadedCount = page * pageSize;
            setHasMore(loadedCount < result.totalCount);
            pageRef.current = page;
        } catch (err) {
            if (requestId === requestIdRef.current) {
                setError(toApiError(err));
            }
        } finally {
            if (requestId === requestIdRef.current) {
                setIsLoading(false);
            }
            isFetchingRef.current = false;
        }
    }, [fetchPage, pageSize]);

    useEffect(() => {
        pageRef.current = 1;
        isFetchingRef.current = false;
        requestIdRef.current++;
        void load(1, true);
    }, [load]);

    const loadMore = useCallback(() => {
        if (!hasMore) return;
        void load(pageRef.current + 1, false);
    }, [hasMore, load]);

    return {items, isLoading, error, hasMore, loadMore};
};