import type {PricedPosition} from "@/shared/types";

export interface CartResponse<T> {
    items: T[];
}

export interface CartPositionPreviewDto {
    productId: number;
    quantity: number;
}

export interface CartPositionDto extends PricedPosition {
    productName: string;
    imageUrl: string;
}