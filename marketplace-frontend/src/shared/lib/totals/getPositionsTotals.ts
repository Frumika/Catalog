import type {PricedPosition} from "@/shared/model";
import {getPositionTotals} from "./getPositionTotals.ts";


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

