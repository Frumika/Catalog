import {ApiError, toApiError} from "@/shared/api";
import {cartApi, useApplyCartPositionUpdate, useClearCartState} from "@/entities/cart";
import {useNotify} from "@/shared/lib";


export const useCartActions = () => {
    const applyPositionUpdate = useApplyCartPositionUpdate();
    const clearCartState = useClearCartState();
    const notify = useNotify();


    const addProduct = async (productId: number) => {
        try {
            const position = await cartApi.addProduct(productId);
            applyPositionUpdate(position);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось добавить товар в корзину");
        }
    };

    const updateQuantity = async (productId: number, quantity: number) => {
        try {
            const position = await cartApi.updateQuantity(productId, quantity);
            applyPositionUpdate(position);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось обновить количество товара");
        }
    };

    const removePosition = async (productId: number) => {
        try {
            const position = await cartApi.removeItem(productId);
            applyPositionUpdate(position);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось удалить товар из корзины");
        }
    };

    const clearCart = async () => {
        try {
            await cartApi.clearCart();
            clearCartState();
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось очистить корзину");
        }
    };

    return {
        addProduct,
        updateQuantity,
        removePosition,
        clearCart
    };
};