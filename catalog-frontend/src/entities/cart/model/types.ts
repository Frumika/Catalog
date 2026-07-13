import type {CartPositionDto, CartPositionPreviewDto} from "../api/dto.ts";

export interface CartPositionPreview extends CartPositionPreviewDto {

}

export interface CartPosition extends Omit<CartPositionDto, 'imageUrl'> {
    imageUrl: string;
}