export interface CartPreviewResult<T> {
    items: T[];
    totalQuantity: number
}

export interface CartResult<T> {
    items: T[];
    totalQuantity: number;
    totalBasePrice: number;
    totalDiscountAmount: number;
    finalPrice: number;
}


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