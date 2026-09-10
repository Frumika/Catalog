import {useAddWishedProduct, useRemoveWishedProduct} from "@/entities/wishlist/model/wishlistStore.ts";
import {ApiError, toApiError} from "@/shared/api";
import {wishlistApi} from "@/entities/wishlist/api/wishlistApi.ts";
import {useNotify} from "@/shared/lib";


export const useWishlistActions = () => {
    const addWishedProduct = useAddWishedProduct();
    const removeWishedProduct = useRemoveWishedProduct();
    const notify = useNotify();


    const addProduct = async (productId: number) => {
        try {
            const product = await wishlistApi.addProduct(productId);
            addWishedProduct(product);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось добавить товар в желаемое");
        }
    };

    const removeProduct = async (productId: number) => {
        try {
            const product = await wishlistApi.removeProduct(productId);
            removeWishedProduct(product);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось убрать товар из желаемого");
        }
    }


    return {
        addProduct,
        removeProduct
    };
}