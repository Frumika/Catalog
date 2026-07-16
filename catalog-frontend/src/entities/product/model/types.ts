import type {ProductDetailsDto, ProductPreviewDto} from "../api/Product.dto.ts";


export interface ProductPreview extends Omit<ProductPreviewDto, 'imageUrl'> {
    imageUrl: string;
}

export interface ProductDetails extends Omit<ProductDetailsDto, 'imageUrls'> {
    imageUrls: string[];
}

export interface ProductFilters {
    categoryId?: number;
    isWishlist?: boolean;
}