import styles from "./PositionsGroupCard.module.css";
import type {OrderPositionGroup} from "@/entities/order";
import {formatGoodsQuantity} from "@/shared/lib/formatGoodsQuantity.ts";
import {formatDate} from "@/shared/lib/formatDate.ts";
import {getGoodsQuantity} from "@/shared/lib";


interface PositionsGroupCardProps {
    positionsGroup: OrderPositionGroup;
    onClick?: () => void;
}

export const PositionsGroupCard = (
    {
        positionsGroup,
        onClick,
    }: PositionsGroupCardProps
) => {

    const totalQuantity = getGoodsQuantity(positionsGroup.orderPositions);
    const positionsQuantityDisplay = formatGoodsQuantity(totalQuantity);
    const deliveryDateDisplay = formatDate(positionsGroup.date);


    return (
        <>

            <div className={styles.checkoutPositionCard}>
                <div className={styles.header}>
                <span className={styles.headerText}
                      onClick={() => onClick?.()}>
                    {`Ожидаемая дата доставки: ${deliveryDateDisplay}`}
                </span>

                    <div className={styles.quantity}>
                        {positionsQuantityDisplay}
                    </div>
                </div>


                <div className={styles.productContainer}>
                    {
                        positionsGroup.orderPositions.map(
                            position =>
                                <img
                                    className={styles.image}
                                    src={position.imageUrl}
                                    alt=""
                                    onClick={() => onClick?.()}
                                />
                        )
                    }
                </div>

                <span className={styles.deliveryType}>
                {"Доставка в пункт выдачи заказов"}
            </span>

            </div>
        </>
    )
}