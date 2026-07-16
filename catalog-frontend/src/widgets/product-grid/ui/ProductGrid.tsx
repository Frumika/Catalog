import styles from "./ProductGrid.module.css";
import {ProductCard, type ProductPreview} from "@/entities/product";
import {AddToCartButton} from "@/features/add-to-cart";
import {ToggleWishedButton} from "@/features/toggle-wished";


interface ProductGridProps {
    products: ProductPreview[];
}

export const ProductGrid = (
    {
        products,
    }: ProductGridProps) => (

    <div className={styles.productGrid}>
        {products.map(product => (
            <ProductCard
                key={product.productId}
                product={product}
                actionSlot={<AddToCartButton productId={product.productId}/>}
                favoriteSlot={<ToggleWishedButton productId={product.productId}/>}
            />
        ))}
    </div>
);