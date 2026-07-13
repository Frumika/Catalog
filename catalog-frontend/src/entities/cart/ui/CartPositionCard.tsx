import type {CartPosition} from "../model/types.ts";
import {useCartActions, usePositionQuantity} from "@/entities/cart";
import styles from "./CartPositionCard.module.css";

import MinusIcon from "@/shared/assets/icons/minus.svg?react";
import PlusIcon from "@/shared/assets/icons/plus.svg?react";
import TrashcanIcon from "@/shared/assets/icons/trashcan.svg?react";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import {formatPrice} from "@/shared/lib";
import {QuantityButton} from "@/shared/ui/quantity-button";


interface CartPositionCard {
    cartPosition: CartPosition;
    onClick?: () => void;
    className?: string;
}

export const CartPositionCard = (
    {
        cartPosition,
        onClick,
        className,

    }: CartPositionCard) => {

    const hasDiscount = cartPosition.discountPercent > 0;
    const {updateQuantity, removeProduct} = useCartActions();
    const positionQuantity = usePositionQuantity(cartPosition.productId);

    return (
        <div className={styles.cartPositionCard}>
            <div className={styles.imageWrapper}>
                <img
                    className={styles.image}
                    src={cartPosition.imageUrl}
                    alt={""}
                />

            </div>


            <div className={styles.contentWrapper}>
                <span>
                    {cartPosition.productName}
                </span>

                <div className={styles.contentButtonWrapper}>

                </div>
            </div>


            <div className={styles.priceWrapper}>
                <span>
                    {`${formatPrice(cartPosition.positionTotal)}₽`}
                </span>

                <span>
                    {`${formatPrice(cartPosition.positionBaseTotal)}₽`}
                </span>
            </div>


            <div className={styles.quantityWrapper}>
                <QuantityButton
                    size="small"
                    quantity={positionQuantity}
                    incQuantity={() => updateQuantity(cartPosition.productId, positionQuantity + 1)}
                    decQuantity={() => updateQuantity(cartPosition.productId, positionQuantity - 1)}
                />
            </div>
        </div>
    );
};