import {useClearWishlist, useSetWishedProducts} from "@/entities/wishlist/model/wishlistStore.ts";
import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {wishlistApi} from "@/entities/wishlist/api/wishlistApi.ts";
import {useNotify} from "@/shared/lib";


export const useWishlistSync = (isAuthenticated: boolean) => {
    const setWishedProducts = useSetWishedProducts();
    const clearWishlist = useClearWishlist();
    const [isLoading, setIsLoading] = useState(false);
    const notify = useNotify();

    useEffect(() => {
        if (!isAuthenticated) {
            clearWishlist();
            return;
        }

        setIsLoading(true);
        wishlistApi.preview()
            .then(result => setWishedProducts(result.items))
            .catch((error) => {
                const apiError: ApiError = toApiError(error);
                notify("error", apiError.message || "Не удалось получить список желаемого");
            })
            .finally(() => setIsLoading(false));
    }, [isAuthenticated]);

    return {isLoading}
}

