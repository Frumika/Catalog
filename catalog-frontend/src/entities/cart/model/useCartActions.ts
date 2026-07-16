import {useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {cartApi, useApplyCartPositionUpdate, useClearCartState} from "@/entities/cart";


export const useCartActions = () => {
    const applyPositionUpdate = useApplyCartPositionUpdate();
    const clearCartState = useClearCartState();

    const [error, setError] = useState<ApiError | null>(null);


    const addProduct = async (productId: number) => {
        setError(null);
        try {
            const position = await cartApi.addProduct(productId);
            applyPositionUpdate(position);
        } catch (error) {
            setError(toApiError(error));
        }
    };

    const updateQuantity = async (productId: number, quantity: number) => {
        setError(null);
        try {
            const position = await cartApi.updateQuantity(productId, quantity);
            applyPositionUpdate(position);
        } catch (error) {
            setError(toApiError(error));
        }
    };

    const removePosition = async (productId: number) => {
        setError(null);
        try {
            const position = await cartApi.removeItem(productId);
            applyPositionUpdate(position);
        } catch (error) {
            setError(toApiError(error));
        }
    };

    const clearCart = async () => {
        setError(null);
        try {
            await cartApi.clearCart();
            clearCartState();
        } catch (error) {
            setError(toApiError(error));
        }
    };

    return {
        addProduct,
        updateQuantity,
        removePosition,
        clearCart,
        error
    };
};