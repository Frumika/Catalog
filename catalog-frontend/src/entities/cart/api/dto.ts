export interface CartResponse<T> {
    items: T[];
}

export interface CartPositionPreviewDto {
    productId: number;
    quantity: number;
}

export interface CartPositionDto {
    productId: number;
    productName: string;
    imageUrl: string;
    basePrice: number;
    discountPercent: number;
    discountedPrice: number;
}