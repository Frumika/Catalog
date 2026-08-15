import styles from "./CheckoutPositionCard.module.css";
import type {OrderPosition} from "@/entities/order";
import {formatGoodsQuantity} from "@/entities/order/model/formatGoodsQuantity.ts";
import {formatDate} from "@/entities/order/model/formatDate.ts";


interface CheckoutPositionCardProps {
    deliveryDate: Date;
    orderPositions: OrderPosition[]
}

export const CheckoutPositionCard = (
    {
        deliveryDate,
        orderPositions,
    }: CheckoutPositionCardProps
) => {

    const positionsQuantity = orderPositions.length;
    const positionsQuantityDisplay = formatGoodsQuantity(positionsQuantity);
    const deliveryDateDisplay = formatDate(deliveryDate);

    return (
        <div className={styles.checkoutPositionCard}>
            <div className={styles.header}>
                <span className={styles.headerText}>
                    {`Ожидаемая дата доставки: ${deliveryDateDisplay}`}
                </span>

                <div className={styles.quantity}>
                    {positionsQuantityDisplay}
                </div>
            </div>


            <div className={styles.productContainer}>
                {
                    orderPositions.map(order =>
                        <img className={styles.image} src={order.imageUrl} alt=""/>
                    )
                }
            </div>

            <span className={styles.deliveryType}>
                {"Доставка в пункт выдачи заказов"}
            </span>

        </div>
    )
}