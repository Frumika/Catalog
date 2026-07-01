import type {ProductPreview} from "../model/Product.types.ts";
import styles from "./ProductCard.module.css";


interface ProductCardProps {
    product?: ProductPreview;
    onClick?: () => void;
    className?: string;
}

export const ProductCard = (
    {
        product,
        onClick,
        className,
    }: ProductCardProps
) => {

    const productCardStyles = [
        styles.productCard,
        className,
    ].filter(Boolean).join(' ');

    return (
        <div className={productCardStyles}>


            <div className={styles.imageWrapper}>
                <a onClick={onClick}>
                    <img
                        src={"../../../shared/assets/images/test.png"}
                        alt={""}
                    />
                </a>
            </div>


            <div className={styles.contentWrapper}>
                <div className={styles.price}>

                </div>

                <span className={styles.productName}>

                </span>

                <div className={styles.feedback}>

                </div>
            </div>


            <button className={styles.wishlistButton}>

            </button>


        </div>
    );
}