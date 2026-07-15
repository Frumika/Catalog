import type {ProductPreview} from "@/entities/product/model/Product.types.ts";
import {ProductCard} from "@/entities/product";
import styles from "./ProductGrid.module.css";


type ProductGridProps = {
    products: ProductPreview[];
};

export const ProductGrid = ({products}: ProductGridProps) => (
    <div className={styles.productGrid}>
        {products.map(product => (
            <ProductCard key={product.productId} product={product} hasButton/>
        ))}
    </div>
);