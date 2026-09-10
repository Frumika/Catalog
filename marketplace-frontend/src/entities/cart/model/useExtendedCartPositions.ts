import {useEffect, useMemo, useState} from "react";
import type {CartPosition} from "./types.ts";
import {useCartPositions} from "@/entities/cart";
import {cartApi} from "../api/cartApi.ts";
import {ApiError, toApiError} from "@/shared/api";
import {useNotify} from "@/shared/lib";


export const useExtendedCartPositions = (isAuthenticated: boolean) => {
    const globalPositions = useCartPositions();
    const [localPositions, setLocalPositions] = useState<CartPosition[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const notify = useNotify();

    useEffect(() => {
        if (!isAuthenticated) {
            setLocalPositions([]);
            return;
        }

        setIsLoading(true);
        cartApi.getCartPositions()
            .then(response => setLocalPositions(response.items))
            .catch(error => {
                const apiError: ApiError = toApiError(error);
                notify("error", apiError.message || "Не удалось получить корзину пользователя");
            })
            .finally(() => setIsLoading(false));
    }, [isAuthenticated]);

    const cartPositions = useMemo(() => {
        const globalMap = new Map(globalPositions.map((gp) => [gp.productId, gp]));

        return localPositions
            .filter((detailed) => globalMap.has(detailed.productId))
            .map((detailed) => ({
                ...detailed,
                quantity: globalMap.get(detailed.productId)!.quantity,
            }));
    }, [localPositions, globalPositions]);
    return {
        cartPositions,
        isLoading,
    };
};