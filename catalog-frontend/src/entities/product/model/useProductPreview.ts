import {useEffect, useState} from "react";
import {productApi} from "../api/productApi.ts";
import type {ProductPreview} from "./Product.types.ts";
import {ApiError} from "@/shared/api";


export const useProductPreview = () => {
    const [products, setProducts] = useState<ProductPreview[]>([]);
    const [error, setError] = useState<ApiError | null>(null);

    useEffect(() => {
        productApi
            .products(1, 5)
            .then(products => setProducts(products))
            .catch(error => setError(error))
            .finally()
    }, []);

    return {
        products,
        error
    };
}