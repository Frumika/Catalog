import {useAddWishedProduct, useRemoveWishedProduct} from "@/entities/wishlist/model/wishlistStore.ts";
import {useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {wishlistApi} from "@/entities/wishlist/api/wishlistApi.ts";


export const useWishlistActions = () => {
    const addWishedProduct = useAddWishedProduct();
    const removeWishedProduct = useRemoveWishedProduct();

    const [error, setError] = useState<ApiError | null>(null);

    const addProduct = async (productId: number) => {
        setError(null);
        try {
            const product = await wishlistApi.addProduct(productId);
            addWishedProduct(product);
        } catch (error) {
            setError(toApiError(error));
        }
    };

    const removeProduct = async (productId: number) => {
        setError(null);
        try {
            const product = await wishlistApi.removeProduct(productId);
            removeWishedProduct(product);
        } catch (error) {
            setError(toApiError(error));
        }
    }

    return {
        addProduct,
        removeProduct,
        error,
    };
}