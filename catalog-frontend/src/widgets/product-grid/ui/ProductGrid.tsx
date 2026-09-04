import styles from "./ProductGrid.module.css";
import {ProductCard, type ProductPreview} from "@/entities/product";
import {AddToCartButton} from "@/features/add-to-cart";
import {ToggleWishedButton} from "@/features/toggle-wished";
import {useNotify} from "@/shared/lib";


interface ProductGridProps {
    products: ProductPreview[];
}

export const ProductGrid = (
    {
        products,
    }: ProductGridProps) => {

    const notify = useNotify();

    const onClick = () => {
        notify("warning", "Страница товара пока не реализована");
    }

    return (
        <div className={styles.productGrid}>
            {products.map(product => (
                <ProductCard
                    key={product.productId}
                    product={product}
                    onClick={onClick}
                    actionSlot={<AddToCartButton productId={product.productId}/>}
                    favoriteSlot={<ToggleWishedButton productId={product.productId} buttonType={'productCard'}/>}
                />
            ))}
        </div>
    )
};