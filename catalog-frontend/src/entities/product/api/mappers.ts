import {getFullUrl} from "@/shared/api";
import type {ProductPreview} from "../model/Product.types.ts";


export const mapProductPreview = (prev: ProductPreview): ProductPreview =>
    (
        {
            ...prev,
            imageUrl: getFullUrl(prev.imageUrl),
        }
    );