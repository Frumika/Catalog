import {create} from "zustand";
import {createJSONStorage, persist} from "zustand/middleware";
import type {Order} from "@/entities/order";


interface CheckoutState {
    activeOrderId: number | null;
    setOrderId: (order: Order) => void;
    clearCheckout: () => void;
}

const useCheckoutStore = create<CheckoutState>()(
    persist(
        (set) => ({
            activeOrderId: null,
            setOrderId: (order: Order) => set({activeOrderId: order.orderId}),
            clearCheckout: () => set({activeOrderId: null}),
        }),
        {
            name: 'checkout_session',
            storage: createJSONStorage(() => sessionStorage),
        }
    )
);

export const useGetCheckoutOrderId = () =>
    useCheckoutStore((state) => state.activeOrderId);

export const useSetActiveCheckoutOrder = () =>
    useCheckoutStore(state => state.setOrderId);

export const useClearCheckout = () =>
    useCheckoutStore(state => state.clearCheckout);