import styles from "./PositionCard.module.css";
import type {OrderPosition} from "@/entities/order";
import {formatPrice} from "@/shared/lib";


interface PositionCardProps {
    orderPosition: OrderPosition;
    isLastCard: boolean;
}

export const PositionCard = (
    {
        orderPosition,
        isLastCard,
    }: PositionCardProps
) => {

    const hasDiscount = orderPosition.discountPercent > 0;
    const positionCardStyles = [
        styles.positionCard,
        !isLastCard ? styles.dividingLine : null,
    ].filter(Boolean).join(' ');

    return (
        <div className={positionCardStyles}>
            <h2 className={styles.sellerName}>
                {orderPosition.sellerName}
            </h2>

            <div className={styles.contentWrapper}>
                <img className={styles.image} src={orderPosition.imageUrl} alt={''}/>

                <span className={styles.productName}>
                    {orderPosition.productName}
                </span>


                <div className={styles.priceWrapper}>
                    {hasDiscount && (
                        <span className={styles.discountedPrice}>
                            {`${formatPrice(orderPosition.discountedPrice)} ₽`}
                        </span>
                    )}

                    <span className={hasDiscount ? styles.oldPrice : styles.freshPrice}>
                        {`${formatPrice(orderPosition.basePrice)} ₽`}
                    </span>
                </div>

                <span className={styles.quantity}>
                    ({orderPosition.quantity} шт.)
                </span>

            </div>
        </div>
    )
}