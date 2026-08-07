import type {CartPosition} from "../model/types.ts";
import {getPositionTotals, useCartActions, useCartPositionQuantity} from "@/entities/cart";
import styles from "./CartPositionCard.module.css";

import TrashcanIcon from "@/shared/assets/icons/trashcan.svg?react";
import {formatPrice} from "@/shared/lib";
import {QuantityButton} from "@/shared/ui/quantity-button";
import {Button} from "@/shared/ui/button";
import {type ReactNode, useMemo} from "react";
import {Checkbox} from "@/shared/ui/checkbox";


interface CartPositionCard {
    cartPosition: CartPosition;
    isSelected?: boolean;
    onClick?: () => void;
    onTogglePosition?: (cartPosition: CartPosition) => void;
    wishButtonSlot?: ReactNode;
    className?: string;
}

export const CartPositionCard = (
    {
        cartPosition,
        isSelected = false,
        onClick,
        onTogglePosition,
        wishButtonSlot,
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
            <div className={styles.activeArea}
                 onClick={() => onClick?.()}>
                <div className={styles.imageWrapper}>
                    <img
                        className={styles.image}
                        src={cartPosition.imageUrl}
                        alt={""}
                    />

                    <Checkbox
                        className={styles.checkbox}
                        selected={isSelected}
                        onChange={() => onTogglePosition?.(cartPosition)}/>

                </div>


                <div className={styles.contentWrapper}>
                <span className={styles.text}>
                    {cartPosition.productName}
                </span>

                    <div className={styles.contentButtonWrapper}>
                        {wishButtonSlot}

                        <Button
                            variant={"neutral"}
                            icon={<TrashcanIcon/>}
                            size={"small"}
                            onClick={async (event) => {
                                event.stopPropagation();
                                await removePosition(cartPosition.productId)
                            }}/>

                        <Button
                            variant={"neutral"}
                            size={"small"}
                            onClick={(event) => {
                                event.stopPropagation();
                            }}>
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