interface Product {
    productId: number;
    productName: string;
    price: number;
    discountPrice: number;
    discountPercent: number;
    reviewCount: number;
    averageScore: number;
}

export interface ProductPreview extends Product {
    imageUrl: string;
}

export interface ProductDetails extends Product {
    productDescription: string | null;
    imageUrls: string[];
    makerName: string;
    makerDescription: string | null;
}