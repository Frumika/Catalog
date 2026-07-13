import {cartApi, useClearCartState, useSetCartPositions} from "@/entities/cart";
import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";

export const useCartSync = (isAuthenticated: boolean) => {
    const setCartPositions = useSetCartPositions();
    const clearCartState = useClearCartState();

    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<ApiError | null>(null);

    useEffect(() => {
        if (!isAuthenticated) {
            clearCartState();
            return;
        }

        setIsLoading(true);
        cartApi.getCartPreview()
            .then((result) => setCartPositions(result.items))
            .catch((error) => setError(toApiError(error)))
            .finally(() => setIsLoading(false));
    }, [isAuthenticated]);

    return { isLoading, error };
};