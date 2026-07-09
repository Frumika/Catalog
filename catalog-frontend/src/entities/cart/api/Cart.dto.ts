export interface CartPositionPreviewDto {
    productId: number;
    quantity: number;
}

export interface CartPositionDto {
    productId: number;
    productName: string;
    imageUrl: string;
    quantity: number;
    basePrice: number;
    discountPercent: number;
    priceWithDiscount: number;
    positionBaseTotal: number;
    positionTotal: number;
    positionDiscountAmount: number;
}