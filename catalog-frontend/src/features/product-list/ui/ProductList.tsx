import type {ProductPreview} from "@/entities/product/model/Product.types.ts";
import {Observer} from "@/shared/ui/observer";
import {ProductCard} from "@/entities/product";
import styles from "./ProductList.module.css";


interface ProductListProps {
    products: ProductPreview[];
    hasMore: boolean;
    onLoadMore: () => void;
}

export const ProductList = (
    {
        products,
        hasMore,
        onLoadMore
    }: ProductListProps) => {

    return (
        <>
            <div className={styles.productList}>
                {products.map(
                    product => (
                        <ProductCard
                            key={product.productId}
                            product={product}
                            hasButton={true}/>
                    ))}
            </div>

            {hasMore && <Observer onIntersect={onLoadMore}/>}
        </>
    );
}