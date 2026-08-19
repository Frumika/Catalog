import type {OrderDto, OrderPositionDto} from "@/entities/order/api/dto.ts";


export interface OrderPosition extends Omit<OrderPositionDto, 'deliveryDate'> {
    imageUrl: string;
    deliveryDate: Date;
}

export interface Order extends Omit<OrderDto, 'createdAt' | 'paidAt'> {
    createdAt: Date;
    paidAt: Date | null;
}

export interface ExtendedOrder extends Order {
    orderPositions: OrderPosition[];
}