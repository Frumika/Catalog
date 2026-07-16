import type {CartPosition} from "../model/types.ts";
import {getPositionTotals, useCartActions, useCartPositionQuantity} from "@/entities/cart";
import styles from "./CartPositionCard.module.css";

import TrashcanIcon from "@/shared/assets/icons/trashcan.svg?react";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import {formatPrice} from "@/shared/lib";
import {QuantityButton} from "@/shared/ui/quantity-button";
import {Button} from "@/shared/ui/button";
import {useMemo} from "react";


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
    const positionQuantity = useCartPositionQuantity(cartPosition.productId);
    const {removePosition, updateQuantity} = useCartActions();

    const {positionBaseTotal, positionDiscountedTotal} = useMemo(
        () => getPositionTotals(cartPosition, positionQuantity),
        [cartPosition, positionQuantity]
    );

    const cartPositionCardStyles =
        [
            styles.cartPositionCard,
            className,
        ].filter(Boolean).join(' ');

    return (
        <div className={cartPositionCardStyles}>
            <div className={styles.imageWrapper}>
                <img
                    className={styles.image}
                    src={cartPosition.imageUrl}
                    alt={""}
                    onClick={() => onClick?.()}
                />
            </div>


            <div className={styles.contentWrapper}
                 onClick={() => onClick?.()}
            >
                <span className={styles.text}>
                    {cartPosition.productName}
                </span>

                <div className={styles.contentButtonWrapper}>
                    <Button
                        className={styles.wishButton}
                        variant={"neutral"}
                        icon={<WishIcon/>}
                        size={"small"}
                    />

                    <Button
                        variant={"neutral"}
                        icon={<TrashcanIcon/>}
                        onClick={() => removePosition(cartPosition.productId)}
                        size={"small"}
                    />

                    <Button
                        variant={"neutral"}
                        size={"small"}>
                        Купить
                    </Button>
                </div>
            </div>


            <div className={styles.priceWrapper}>
                {hasDiscount && (
                    <span className={styles.discountPrice}>
                        {`${formatPrice(positionDiscountedTotal)}₽`}
                    </span>
                )}

                <span className={hasDiscount ? styles.oldPrice : styles.freshPrice}>
                    {`${formatPrice(positionBaseTotal)}₽`}
                </span>
            </div>


            <div className={styles.quantityWrapper}>
                <QuantityButton
                    size="small"
                    variant={"neutral"}
                    quantity={positionQuantity}
                    incQuantity={() => updateQuantity(cartPosition.productId, positionQuantity + 1)}
                    decQuantity={() => updateQuantity(cartPosition.productId, positionQuantity - 1)}
                />
            </div>
        </div>
    );
};