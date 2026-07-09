import type {CartPositionDto} from "../api/Cart.dto.ts";

export interface CartPosition extends Omit<CartPositionDto, 'imageUrl'> {
    imageUrl: string;
}