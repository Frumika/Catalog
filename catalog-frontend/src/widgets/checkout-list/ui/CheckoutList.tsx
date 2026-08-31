import styles from "./CheckoutList.module.css";
import {PositionsGroupCard, type ExtendedOrder, type OrderPosition, type OrderPositionGroup} from "@/entities/order";
import {useMemo} from 'react';


interface CheckoutListProps {
    order: ExtendedOrder;
    onGroupSelect: (positionGroup: OrderPositionGroup) => void;
}

export const CheckoutList = (
    {
        order,
        onGroupSelect,
    }: CheckoutListProps
) => {
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
                <PositionsGroupCard
                    key={opg.date.getTime()}
                    positionsGroup={opg}
                    onClick={() => onGroupSelect(opg)}
                />
            )}
        </section>

    );
};