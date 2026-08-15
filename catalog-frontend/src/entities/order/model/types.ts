import type {ExtendedOrderDto, OrderDto, OrderPositionDto} from "@/entities/order/api/dto.ts";

export interface OrderPosition extends OrderPositionDto {
    imageUrl: string;
}

export interface Order extends Omit<OrderDto, 'createdAt' | 'paidAt' | 'deliveryDate'> {
    createdAt: Date;
    paidAt: Date | null;
    deliveryDate: Date;
}

export interface ExtendedOrder extends Order {
    orderPositions: OrderPosition[];
}