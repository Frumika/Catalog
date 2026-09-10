import {createContext, type ReactNode, useContext} from "react";
import type {CartPosition} from "@/entities/cart";
import {useCartSelection} from "./useCartSelection.ts";
import {useNotify} from "@/shared/lib";


type CartSelectionContextType = ReturnType<typeof useCartSelection>;

const CartSelectionContext = createContext<CartSelectionContextType | null>(null);

interface CartSelectionProviderProps {
    children: ReactNode;
    cartPositions: CartPosition[];
}

export const CartSelectionProvider = (
    {
        children,
        cartPositions,
    }: CartSelectionProviderProps) => {

    const selection = useCartSelection(cartPositions);

    return (
        <CartSelectionContext.Provider value={selection}>
            {children}
        </CartSelectionContext.Provider>
    );
};

export const useCartSelectionContext = () => {
    const context = useContext(CartSelectionContext);
    const notify = useNotify();

    if (!context) {
        notify("error", "Что-то пошло не так");
        throw new Error("useCartSelectionContext must be used within CartSelectionProvider");
    }
    return context;
};