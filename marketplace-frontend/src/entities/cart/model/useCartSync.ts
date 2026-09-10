import {cartApi, useClearCartState, useSetCartPositions} from "@/entities/cart";
import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {useNotify} from "@/shared/lib";


export const useCartSync = (isAuthenticated: boolean) => {
    const setCartPositions = useSetCartPositions();
    const clearCartState = useClearCartState();
    const notify = useNotify();
    const [isLoading, setIsLoading] = useState(false);


    useEffect(() => {
        if (!isAuthenticated) {
            clearCartState();
            return;
        }

        setIsLoading(true);
        cartApi.getCartPreview()
            .then((result) => setCartPositions(result.items))
            .catch(error => {
                const apiError: ApiError = toApiError(error);
                notify("error", apiError.message || "Не удалось получить корзину пользователя");
            })
            .finally(() => setIsLoading(false));
    }, [isAuthenticated]);

    return {isLoading};
};