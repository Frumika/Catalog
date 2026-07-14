import {useEffect, useMemo, useState} from "react";
import type {CartPosition, CartPositionPreview} from "./types.ts";
import {useCartActions, useCartPositions} from "@/entities/cart";
import {cartApi} from "../api/cartApi.ts";
import {ApiError, toApiError} from "@/shared/api";


export const useCartPage = (isAuthenticated: boolean) => {
    const globalPositions = useCartPositions();
    const [detailedPositions, setDetailedPositions] = useState<CartPosition[]>([]);
    const [error, setError] = useState<ApiError | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        if (!isAuthenticated) {
            setDetailedPositions([]);
            return;
        }

        setIsLoading(true);
        cartApi.getCartPositions()
            .then(response => setDetailedPositions(response.items))
            .catch(error => setError(toApiError(error)))
            .finally(() => setIsLoading(false));
    }, [isAuthenticated]);

    const cartPositions = useMemo(() => {
        const activeIds = new Set(globalPositions.map((gp) => gp.productId));

        return detailedPositions.filter((detailed) =>
            activeIds.has(detailed.productId)
        );
    }, [detailedPositions, globalPositions]);

    return {
        cartPositions,
        isLoading,
        error,
    };
};