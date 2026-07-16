import {useClearWishlist, useSetWishedProducts} from "@/entities/wishlist/model/wishlistStore.ts";
import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {wishlistApi} from "@/entities/wishlist/api/wishlistApi.ts";


export const useWishlistSync = (isAuthenticated: boolean) => {
    const setWishedProducts = useSetWishedProducts();
    const clearWishlist = useClearWishlist();

    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<ApiError | null>(null);

    useEffect(() => {
        if (!isAuthenticated) {
            clearWishlist();
            return;
        }

        setIsLoading(true);
        wishlistApi.preview()
            .then(result => setWishedProducts(result.items))
            .catch((error) => setError(toApiError(error)))
            .finally(() => setIsLoading(false));
    }, [isAuthenticated]);

    return {
        isLoading,
        error,
    }
}

