export interface ProductDto {
    productId: number;
    productName: string;
    price: number;
    discountPrice: number;
    discountPercent: number;
    reviewCount: number;
    averageScore: number;
}

export interface ProductPreviewDto extends ProductDto {
    imageUrl: string | null;
}

export interface ProductDetailsDto extends ProductDto {
    productDescription: string | null;
    imageUrls: string[];
    makerName: string;
    makerDescription: string | null;
}