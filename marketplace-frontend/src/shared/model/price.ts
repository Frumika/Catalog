export interface PricedPosition {
    productId: number;
    quantity: number;
    basePrice: number;
    discountPercent: number;
    discountedPrice: number;
}