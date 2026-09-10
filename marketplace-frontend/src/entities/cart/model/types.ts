import type {CartPositionDto, CartPositionPreviewDto} from "../api/dto.ts";
import type {PricedPosition} from "@/shared/types";


export interface CartPositionPreview extends CartPositionPreviewDto {
}

export interface CartPosition extends Omit<CartPositionDto, 'imageUrl' | 'basePrice' | 'discountPercent' | 'discountedPrice'>, PricedPosition {
    imageUrl: string;
}