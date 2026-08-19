import type {PricedPosition} from "@/shared/types";


export const getPositionTotals = (position: PricedPosition, quantity: number) => {
    const positionBaseTotal = position.basePrice * quantity;
    const positionDiscountedTotal = position.discountedPrice * quantity;
    return {
        positionBaseTotal,
        positionDiscountedTotal,
        positionDiscountAmount: positionBaseTotal - positionDiscountedTotal,
    };
};

export const getPositionsTotals = (positions: PricedPosition[]) => {
    return positions.reduce(
        (acc, position) => {
            const t = getPositionTotals(position, position.quantity);
            return {
                totalBasePrice: acc.totalBasePrice + t.positionBaseTotal,
                totalDiscountAmount: acc.totalDiscountAmount + t.positionDiscountAmount,
                totalDiscountedPrice: acc.totalDiscountedPrice + t.positionDiscountedTotal,
            };
        },
        {totalBasePrice: 0, totalDiscountAmount: 0, totalDiscountedPrice: 0}
    );
}

export const getTotalQuantity = (positions: PricedPosition[]) => {
    return positions.reduce((acc: number, pos: PricedPosition) => acc + pos.quantity, 0);
};
