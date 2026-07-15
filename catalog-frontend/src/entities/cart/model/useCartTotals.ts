import type {CartPosition} from "@/entities/cart/model/types.ts";
import {useCartPositions} from "@/entities/cart";
import {getCartTotals} from "@/entities/cart/model/pricing.ts";
import {useMemo} from "react";


export const useCartTotals = (positions: CartPosition[]) => {
    const globalPositions = useCartPositions();

    return useMemo(() => {
        if (!positions) return {totalBasePrice: 0, totalDiscountAmount: 0, totalDiscountedPrice: 0};
        const quantityMap = new Map(globalPositions.map((p) => [p.productId, p.quantity]));
        return getCartTotals(positions, quantityMap);
    }, [positions, globalPositions]);
}