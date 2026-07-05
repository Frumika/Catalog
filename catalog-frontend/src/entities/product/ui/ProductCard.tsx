import type {ProductPreview} from "../model/Product.types.ts";
import styles from "./ProductCard.module.css";

import {Button} from "@/shared/ui/button";
import {Icon} from "@/shared/ui/icon";

import StarIcon from "@/shared/assets/icons/star.svg?react";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import ReviewIcon from "@/shared/assets/icons/message.svg?react";
import CartIcon from "@/shared/assets/icons/cart.svg?react";


interface ProductCardProps {
    product: ProductPreview;
    hasButton?: boolean;
    onClick?: () => void;
    className?: string;
}

export const ProductCard = (
    {
        product,
        hasButton = true,
        onClick,
        className,
    }: ProductCardProps
) => {

    const productCardStyles = [
        styles.productCard,
        className,
    ].filter(Boolean).join(' ');

    const hasDiscount = product.discountPercent !== 0;
    const hasReview = product.reviewCount > 0;

    return (
        <div className={productCardStyles}>
            <a className={styles.imageWrapper}>
                <img
                    className={styles.image}
                    onClick={onClick}
                    src={product.imageUrl}
                    alt={""}
                />
            </a>

            <div className={styles.contentWrapper}>
                <div className={styles.priceContainer}>
                    {hasDiscount &&
                        <span className={styles.discountPrice}>
                            {`${product.discountPrice}₽`}
                        </span>}

                    <span className={hasDiscount ? styles.oldPrice : styles.price}>
                        {`${product.price}₽`}
                    </span>

                    {hasDiscount &&
                        <span className={styles.discountPercent}>
                            {`-${product.discountPercent}%`}
                        </span>}
                </div>

                <span className={styles.productName}
                      onClick={onClick}>
                    {product.productName}
                </span>

                {hasReview &&
                    <div className={styles.feedback}>
                        <Icon className={styles.starIcon}
                              size={"small"}>
                            <StarIcon/>
                        </Icon>

                        <span className={styles.averageScore}>
                        {product.averageScore}
                    </span>

                        <Icon className={styles.reviewIcon}
                              size={"small"}>
                            <ReviewIcon/>
                        </Icon>

                        <span className={styles.reviewCountText}>
                        {`${product.reviewCount} отзывов`}
                    </span>
                    </div>
                }

                {hasButton &&
                    <div className={styles.cartButton}>
                        <Button
                            fullWidth={true}
                            size={"small"}
                            icon={<CartIcon/>}>
                            В корзину
                        </Button>
                    </div>}
            </div>

            <button className={styles.wishlistButton}
                    onClick={(event) => {
                        event.stopPropagation();
                    }}>
                <Icon><WishIcon/></Icon>
            </button>
        </div>
    );
}