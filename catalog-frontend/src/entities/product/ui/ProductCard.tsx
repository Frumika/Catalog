import type {ProductPreview} from "../model/Product.types.ts";
import styles from "./ProductCard.module.css";

import {formatPrice} from "@/shared/lib";
import {Button} from "@/shared/ui/button";
import {Icon} from "@/shared/ui/icon";
import {QuantityButton} from "./quantity-button/QuantityButton.tsx";

import StarIcon from "@/shared/assets/icons/star.svg?react";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import ReviewIcon from "@/shared/assets/icons/message.svg?react";
import CartIcon from "@/shared/assets/icons/cart.svg?react";
import {useCartStore} from "@/entities/cart-item";


interface ProductCardProps {
    product: ProductPreview;
    hasButton?: boolean;
    onClick?: () => void;
    className?: string;
}

export const ProductCard = ({product, hasButton = true, onClick, className}: ProductCardProps) => {
    // Получаем состояние и экшены из общего стора корзины
    const cartItems = useCartStore(state => state.items);
    const addToCart = useCartStore(state => state.addToCart);
    const updateQuantity = useCartStore(state => state.updateQuantity);

    // Ищем, добавлен ли этот конкретный товар в корзину
    const cartInPosition = cartItems.find(item => item.productId === product.productId);
    const quantityInCart = cartInPosition ? cartInPosition.quantity : 0;

    const productCardStyles = [styles.productCard, className].filter(Boolean).join(' ');
    const hasDiscount = product.discountPercent !== 0;
    const hasReview = product.reviewCount > 0;

    // Клик по кнопке "В корзину" (первое добавление)
    const handleFirstAdd = (event: React.MouseEvent) => {
        event.stopPropagation();
        addToCart(product.productId);
    };

    // Клик по кнопкам контроля количества (+ / -)
    const handleQuantityChange = (event: React.MouseEvent, action: 'increment' | 'decrement') => {
        event.stopPropagation();
        updateQuantity(product.productId, quantityInCart, action);
    };

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

                {hasButton && (
                    <div className={styles.cartButtonWrapper}>
                        {quantityInCart > 0 ?
                            <QuantityButton
                                size="small"
                                quantity={quantityInCart}
                                incQuantity={handleQuantityChange}
                                decQuantity={handleQuantityChange}
                            />
                            :
                            <Button
                                fullWidth={true}
                                size="small"
                                icon={<CartIcon/>}
                                onClick={handleFirstAdd}
                            >
                                В корзину
                            </Button>
                        }
                    </div>
                )}
            </div>

            <button className={styles.wishlistButton} onClick={(e) => e.stopPropagation()}>
                <Icon className={styles.wishlistIcon}><WishIcon/></Icon>
            </button>
        </div>
    );
};