import {getFullUrl} from "@/shared/api";
import type {ProductDetails, ProductPreview} from "../model/Product.types.ts";
import type {ProductDetailsDto, ProductPreviewDto} from "./Product.dto.ts";
import PlaceholderImage from "@/shared/assets/images/placeholder.png";


export const mapProductPreview = (dto: ProductPreviewDto): ProductPreview => ({
    ...dto,
    imageUrl: dto.imageUrl ? getFullUrl(dto.imageUrl) : PlaceholderImage,
});

export const mapProductDetails = (dto: ProductDetailsDto): ProductDetails => ({
    ...dto,
    imageUrls: dto.imageUrls.length > 0
        ? dto.imageUrls.map(getFullUrl)
        : [PlaceholderImage],
});