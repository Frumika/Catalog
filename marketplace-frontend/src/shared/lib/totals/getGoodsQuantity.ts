import type {PricedPosition} from "@/shared/model";

export const getGoodsQuantity = (positions: PricedPosition[]) => {
    return positions.reduce((acc: number, pos: PricedPosition) => acc + pos.quantity, 0);
};