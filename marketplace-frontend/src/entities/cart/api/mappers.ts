import {getFullUrl} from "@/shared/api";
import type {CartPosition} from "../model/types.ts";
import type {CartPositionDto} from "./dto.ts";
import PlaceholderImage from "@/shared/assets/images/placeholder.png";


export const mapCartPosition = (dto: CartPositionDto): CartPosition => ({
    ...dto,
    imageUrl: dto.imageUrl ? getFullUrl(dto.imageUrl) : PlaceholderImage,
});