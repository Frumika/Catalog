import { create } from 'zustand';
import { cartApi } from '../api/cartApi';
import type { CartPosition } from '../model/types';

interface CartState {
    items: CartPosition[];
    loading: boolean;
    fetchCart: () => Promise<void>;
    updateQuantity: (productId: number, currentQuantity: number, action: 'increment' | 'decrement') => Promise<void>;
    addToCart: (productId: number) => Promise<void>;
}

export const useCartStore = create<CartState>((set, get) => ({
    items: [],
    loading: false,

    // Загрузка актуального состояния корзины с бэкенда
    fetchCart: async () => {
        set({ loading: true });
        try {
            const data = await cartApi.getCartPositions();
            set({ items: data.items });
        } catch (err) {
            console.error("Ошибка загрузки корзины в стор:", err);
        } finally {
            set({ loading: false });
        }
    },

    // Добавление первого товара (количество = 1)
    addToCart: async (productId: number) => {
        // Оптимистичное обновление: сразу добавляем в локальный массив для мгновенного отклика UI
        const tempItem: CartPosition = {
            productId,
            quantity: 1,
            productName: '', // Опциональные поля, бэкенд все равно вернет актуальные при fetchCart
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
            await get().fetchCart(); // Синхронизируем точные данные с бэкенда
        } catch (err) {
            console.error(err);
            // Если сервер ответил ошибкой, откатываем изменения
            await get().fetchCart();
        }
    },

    // Изменение количества или удаление, если дошли до 0
    updateQuantity: async (productId: number, currentQuantity: number, action: 'increment' | 'decrement') => {
        const nextQuantity = action === 'increment' ? currentQuantity + 1 : currentQuantity - 1;

        if (nextQuantity <= 0) {
            // Оптимистичное удаление
            set((state) => ({ items: state.items.filter(i => i.productId !== productId) }));
            try {
                await cartApi.removeItem(productId);
            } catch (err) {
                console.error(err);
                await get().fetchCart();
            }
            return;
        }

        // Оптимистичное изменение количества
        set((state) => ({
            items: state.items.map(i => i.productId === productId ? { ...i, quantity: nextQuantity } : i)
        }));

        try {
            await cartApi.updateQuantity(productId, nextQuantity);
        } catch (err) {
            console.error(err);
            await get().fetchCart();
        }
    }
}));