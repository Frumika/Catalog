import {useEffect, useMemo, useState} from "react";
import type {CartPosition} from "./types.ts";
import {useCartPositions} from "@/entities/cart";
import {cartApi} from "../api/cartApi.ts";
import {ApiError, toApiError} from "@/shared/api";


export const useExtendedCartPositions = (isAuthenticated: boolean) => {
    const globalPositions = useCartPositions();
    const [localPositions, setLocalPositions] = useState<CartPosition[]>([]);
    const [error, setError] = useState<ApiError | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        if (!isAuthenticated) {
            setLocalPositions([]);
            return;
        }

        setIsLoading(true);
        cartApi.getCartPositions()
            .then(response => setLocalPositions(response.items))
            .catch(error => setError(toApiError(error)))
            .finally(() => setIsLoading(false));
    }, [isAuthenticated]);

    const cartPositions = useMemo(() => {
        const activeIds = new Set(globalPositions.map((gp) => gp.productId));

        return localPositions.filter((detailed) =>
            activeIds.has(detailed.productId)
        );
    }, [localPositions, globalPositions]);

    return {
        cartPositions,
        isLoading,
        error,
    };
};