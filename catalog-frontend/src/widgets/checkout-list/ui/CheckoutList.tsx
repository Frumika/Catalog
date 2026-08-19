import styles from "./CheckoutList.module.css";
import {CheckoutPositionCard, type ExtendedOrder, type OrderPosition} from "@/entities/order";
import {useMemo} from 'react';


interface CheckoutListProps {
    order: ExtendedOrder;
}

interface OrderPositionGroup {
    date: Date;
    orderPositions: OrderPosition[];
}

export const CheckoutList = ({order}: CheckoutListProps) => {
    const orderPositionsGroups = useMemo(() => {

        const groupsRecord = order.orderPositions.reduce((acc, position) => {
            const timeKey = position.deliveryDate.getTime();

            if (!acc[timeKey]) {
                acc[timeKey] = [];
            }

            acc[timeKey].push(position);
            return acc;
        }, {} as Record<number, OrderPosition[]>);

        const groupsArray: OrderPositionGroup[] = Object.entries(groupsRecord).map(
            ([timeStr, positions]) => ({
                date: new Date(Number(timeStr)),
                orderPositions: positions,
            })
        );

        return groupsArray.sort((a, b) => a.date.getTime() - b.date.getTime());

    }, [order.orderPositions]);


    return (
        <section className={styles.checkoutList}>
            {orderPositionsGroups.map(opg =>
                <CheckoutPositionCard
                    key={opg.date.getTime()}
                    deliveryDate={opg.date}
                    orderPositions={opg.orderPositions}
                />
            )}
        </section>
    );
};