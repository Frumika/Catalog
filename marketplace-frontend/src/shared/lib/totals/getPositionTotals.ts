import type {PricedPosition} from "@/shared/model";

export const getPositionTotals = (position: PricedPosition, quantity: number) => {
    const positionBaseTotal = position.basePrice * quantity;
    const positionDiscountedTotal = position.discountedPrice * quantity;
    return {
        positionBaseTotal,
        positionDiscountedTotal,
        positionDiscountAmount: positionBaseTotal - positionDiscountedTotal,
    };
};