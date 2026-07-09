import { create } from 'zustand';
import { cartApi } from '../api/cartApi.ts';
import type { CartPosition } from './types.ts';

// Расширяем тип CartPosition локально для стора, чтобы добавить свойство checked
export interface ExtendedCartPosition extends CartPosition {
    checked: boolean;
}

interface CartState {
    items: ExtendedCartPosition[]; // Изменяем тип на расширенный
    loading: boolean;
    fetchCart: () => Promise<void>;
    updateQuantity: (productId: number, currentQuantity: number, action: 'increment' | 'decrement') => Promise<void>;
    addToCart: (productId: number) => Promise<void>;
    removeItem: (productId: number) => Promise<void>;
    toggleCheck: (productId: number) => void; // <-- Новый метод для чекбокса
}

export const useCartStore = create<CartState>((set, get) => ({
    items: [],
    loading: false,

    fetchCart: async () => {
        set({ loading: true });
        try {
            const data = await cartApi.getCartPositions();
            // Каждому пришедшему товару по умолчанию ставим checked: true
            const extendedItems = data.items.map(item => ({ ...item, checked: true }));
            set({ items: extendedItems });
        } catch (err) {
            console.error("Ошибка загрузки корзины в стор:", err);
        } finally {
            set({ loading: false });
        }
    },

    addToCart: async (productId: number) => {
        const tempItem: ExtendedCartPosition = {
            productId,
            quantity: 1,
            checked: true, // По умолчанию выбран
            productName: '',
            imageUrl: '',
            priceWithDiscount: 0,
            basePrice: 0,
            discountPercent: 0,
            positionBaseTotal: 0,
            positionTotal: 0,
            positionDiscountAmount: 0
        };

        set((state) => ({ items: [...state.items, tempItem] }));

        try {
            await cartApi.addProductToCart(productId, 1);
            await get().fetchCart();
        } catch (err) {
            console.error(err);
            await get().fetchCart();
        }
    },

    updateQuantity: async (productId: number, currentQuantity: number, action: 'increment' | 'decrement') => {
        const nextQuantity = action === 'increment' ? currentQuantity + 1 : currentQuantity - 1;

        if (nextQuantity <= 0) {
            set((state) => ({ items: state.items.filter(i => i.productId !== productId) }));
            try {
                await cartApi.removeItem(productId);
            } catch (err) {
                console.error(err);
                await get().fetchCart();
            }
            return;
        }

        set((state) => ({
            items: state.items.map(i => i.productId === productId ? { ...i, quantity: nextQuantity } : i)
        }));

        try {
            await cartApi.updateQuantity(productId, nextQuantity);
        } catch (err) {
            console.error(err);
            await get().fetchCart();
        }
    },

    removeItem: async (productId: number) => {
        set((state) => ({
            items: state.items.filter(item => item.productId !== productId)
        }));

        try {
            await cartApi.removeItem(productId);
        } catch (err) {
            console.error(err);
            await get().fetchCart();
        }
    },

    // Логика переключения чекбокса (работает мгновенно на клиенте)
    toggleCheck: (productId: number) => {
        set((state) => ({
            items: state.items.map(item =>
                item.productId === productId ? { ...item, checked: !item.checked } : item
            )
        }));
    }
}));