export interface OrderPositionDto {
    productId: number;
    imageUrl: string | null;
    productName: string;
    quantity: number;
    price: number;
    discountPercent: number;
    discountPrice: number;
}

export interface OrderDto {
    orderId: number;
    status: string;
    createdAt: string;
    paidAt: string | null;
    deliveryDate: string;
}

export interface ExtendedOrderDto extends OrderDto {
    orderPositions: OrderPositionDto[];
}