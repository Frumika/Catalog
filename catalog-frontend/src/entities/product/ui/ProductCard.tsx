import type {ProductPreview} from "../model/types.ts";
import styles from "./ProductCard.module.css";

import {formatPrice} from "@/shared/lib";
import {Icon} from "@/shared/ui/icon";

import StarIcon from "@/shared/assets/icons/star.svg?react";
import ReviewIcon from "@/shared/assets/icons/message.svg?react";

import type {ReactNode} from "react";


interface ProductCardProps {
    product: ProductPreview;
    actionSlot?: ReactNode;
    favoriteSlot?: ReactNode;
    onClick?: () => void;
    className?: string;
}

export const ProductCard = (
    {
        product,
        actionSlot,
        favoriteSlot,
        onClick,
        className
    }: ProductCardProps) => {
    const productCardStyles = [styles.productCard, className].filter(Boolean).join(' ');
    const hasDiscount = product.discountPercent !== 0;
    const hasReview = product.reviewCount > 0;

    return (
        <div className={productCardStyles}>
            <a className={styles.imageWrapper}>
                <img className={styles.image} onClick={onClick} src={product.imageUrl} alt=""/>
            </a>

            <div className={styles.contentWrapper}>
                <div className={styles.priceContainer}>
                    {hasDiscount && (
                        <span className={styles.discountPrice}>
                            {`${formatPrice(product.discountPrice)}₽`}
                        </span>
                    )}
                    <span className={hasDiscount ? styles.oldPrice : styles.price}>
                        {`${formatPrice(product.price)}₽`}
                    </span>
                    {hasDiscount && (
                        <span className={styles.discountPercent}>
                            {`-${product.discountPercent}%`}
                        </span>
                    )}
                </div>

                <span className={styles.productName} onClick={onClick}>
                    {product.productName}
                </span>

                {hasReview && (
                    <div className={styles.feedback}>
                        <Icon className={styles.starIcon} size="small"><StarIcon/></Icon>
                        <span className={styles.averageScore}>{product.averageScore}</span>
                        <Icon className={styles.reviewIcon} size="small"><ReviewIcon/></Icon>
                        <span className={styles.reviewCountText}>{`${product.reviewCount} отзывов`}</span>
                    </div>
                )}

                {actionSlot && (
                    <div className={styles.cartButtonWrapper}>
                        {actionSlot}
                    </div>
                )}
            </div>

            {favoriteSlot && (
                <div className={styles.wishlistButton}
                     onClick={(e) => e.stopPropagation()}>
                    {favoriteSlot}
                </div>
            )}
        </div>
    );
};