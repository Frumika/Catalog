import type {ExtendedOrderDto, OrderDto, OrderPositionDto} from "@/entities/order/api/dto.ts";
import type {ExtendedOrder, Order, OrderPosition} from "@/entities/order";
import {getFullUrl} from "@/shared/api";
import PlaceholderImage from "@/shared/assets/images/placeholder.png";


export const mapOrderPosition = (dto: OrderPositionDto): OrderPosition => ({
    ...dto,
    imageUrl: dto.imageUrl ? getFullUrl(dto.imageUrl) : PlaceholderImage,
});

export const mapOrder = (dto: OrderDto): Order => ({
    ...dto,
    createdAt: new Date(dto.createdAt),
    paidAt: dto.paidAt ? new Date(dto.paidAt) : null,
    deliveryDate: new Date(dto.deliveryDate),
});

export const mapExtendedOrder = (dto: ExtendedOrderDto): ExtendedOrder => ({
    ...mapOrder(dto),
    orderPositions: dto.orderPositions.map(mapOrderPosition),
});