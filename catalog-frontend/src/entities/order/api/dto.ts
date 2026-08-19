import type {PricedPosition} from "@/shared/types";

export interface OrderPositionDto extends PricedPosition {
    imageUrl: string | null;
    productName: string;
    deliveryDate: string;
}

export interface OrderDto {
    orderId: number;
    status: string;
    createdAt: string;
    paidAt: string | null;
}

export interface ExtendedOrderDto extends OrderDto {
    orderPositions: OrderPositionDto[];
}