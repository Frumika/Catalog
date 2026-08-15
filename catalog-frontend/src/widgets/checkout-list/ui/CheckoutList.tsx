import styles from "./CheckoutList.module.css";
import {CheckoutPositionCard, type ExtendedOrder} from "@/entities/order";


interface CheckoutListProps {
    order: ExtendedOrder;
}

export const CheckoutList = (
    {
        order
    }: CheckoutListProps
) => {

    return (
        <section className={styles.checkoutList}>
            <CheckoutPositionCard
                deliveryDate={order.deliveryDate}
                orderPositions={order.orderPositions}
            />

        </section>
    );
}