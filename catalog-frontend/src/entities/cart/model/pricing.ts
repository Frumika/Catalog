import type {CartPosition} from "@/entities/cart/model/types.ts";


export const getPositionTotals = (position: CartPosition, quantity: number) => {
    const positionBaseTotal = position.basePrice * quantity;
    const positionDiscountedTotal = position.discountedPrice * quantity;
    return {
        positionBaseTotal,
        positionDiscountedTotal,
        positionDiscountAmount: positionBaseTotal - positionDiscountedTotal,
    };
};

export const getCartTotals = (positions: CartPosition[], quantityMap: Map<number, number>) =>
    positions.reduce(
        (acc, position) => {
            const quantity = quantityMap.get(position.productId) ?? 0;
            const t = getPositionTotals(position, quantity);
            return {
                totalBasePrice: acc.totalBasePrice + t.positionBaseTotal,
                totalDiscountAmount: acc.totalDiscountAmount + t.positionDiscountAmount,
                totalDiscountedPrice: acc.totalDiscountedPrice + t.positionDiscountedTotal,
            };
        },
        {totalBasePrice: 0, totalDiscountAmount: 0, totalDiscountedPrice: 0}
    );