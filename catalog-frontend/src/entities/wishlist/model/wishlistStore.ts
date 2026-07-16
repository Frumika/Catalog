import {create} from "zustand";
import type {WishedProduct} from "@/entities/wishlist/model/types.ts";


interface WishlistState {
    products: WishedProduct[];
}

interface WishlistActions {
    setProducts: (products: WishedProduct[]) => void;
    addProduct: (product: WishedProduct) => void;
    removeProduct: (product: WishedProduct) => void;
    clearWishlist: () => void;
}

export const useWishlistStore = create<WishlistState & WishlistActions>((set) => ({
    products: [],

    setProducts: (products: WishedProduct[]) => set({products}),

    addProduct: (product: WishedProduct) => set((state) => {
        const exist = state.products.some(p => p.productId == product.productId);

        return {
            products: !exist ? [...state.products, product] : state.products
        };
    }),

    removeProduct: (product: WishedProduct) => set((state) => {
        const exist = state.products.some(p => p.productId == product.productId);

        return {
            products: exist ? state.products.filter(p => p.productId != product.productId) : state.products
        };
    }),

    clearWishlist: () => set({products: []}),
}));


export const useWishlist = () =>
    useWishlistStore(state => state.products);

export const useIsProductWished = (productId: number) =>
    useWishlistStore(state => state.products.some(p => p.productId == productId));

export const useWishlistTotalQuantity = () =>
    useWishlistStore(state => state.products.reduce((sum: number) => sum + 1, 0));

export const useSetWishedProducts = () =>
    useWishlistStore(state => state.setProducts);

export const useAddWishedProduct = () =>
    useWishlistStore(state => state.addProduct);

export const useRemoveWishedProduct = () =>
    useWishlistStore(state => state.removeProduct);

export const useClearWishlist = () =>
    useWishlistStore(state => state.clearWishlist);