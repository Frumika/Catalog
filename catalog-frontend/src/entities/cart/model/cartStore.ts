import {create} from 'zustand';
import type {CartPositionPreview} from "@/entities/cart/model/types.ts";


interface CartState {
    cartPositions: CartPositionPreview[];
}

interface CartActions {
    setCartPositions: (cartPositions: CartPositionPreview[]) => void;
    applyPositionUpdate: (position: CartPositionPreview) => void;
    clearCart: () => void;
}

export const useCartStore = create<CartState & CartActions>((set) => ({
    cartPositions: [],

    setCartPositions: (cartPositions) => set({cartPositions}),

    applyPositionUpdate: (position) => set((state) => {
        const exists = state.cartPositions.some(
            (item) => item.productId === position.productId
        );

        if (position.quantity === 0) {
            if (!exists) {
                return {};
            }
            return {
                cartPositions: state.cartPositions.filter(
                    (item) => item.productId !== position.productId
                ),
            };
        }

        if (!exists) {
            return {cartPositions: [...state.cartPositions, position]};
        }

        return {
            cartPositions: state.cartPositions.map((item) =>
                item.productId === position.productId ? position : item
            ),
        };
    }),

    clearCart: () => set({cartPositions: []}),
}));


export const useCartPositions = () =>
    useCartStore((s) => s.cartPositions);

export const useCartTotalQuantity = () =>
    useCartStore((s) =>
        s.cartPositions.reduce((sum, p) => sum + p.quantity, 0));

export const useCartPosition = (productId: number) =>
    useCartStore((s) =>
        s.cartPositions.find((p) => p.productId === productId));

export const useCartPositionQuantity = (productId: number) =>
    useCartStore((s) =>
    s.cartPositions.find((p) => p.productId === productId)?.quantity ?? 0);

export const useSetCartPositions = () =>
    useCartStore((s) => s.setCartPositions);

export const useApplyCartPositionUpdate = () =>
    useCartStore((s) => s.applyPositionUpdate);

export const useClearCartState = () =>
    useCartStore((s) => s.clearCart);