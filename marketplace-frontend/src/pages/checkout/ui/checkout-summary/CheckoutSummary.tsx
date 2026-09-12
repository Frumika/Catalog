import styles from "./CheckoutSummary.module.css";
import {Button} from "@/shared/ui/button";
import {Summary} from "@/shared/ui/summary";
import type {PricedPosition} from "@/shared/model";


interface CheckoutSummaryProps {
    orderPositions: PricedPosition[];
    onPay?: () => void;
}

export const CheckoutSummary = (
    {
        orderPositions,
        onPay,
    }: CheckoutSummaryProps
) => {

    return (
        <Summary
            title={"Ваш заказа"}
            positions={orderPositions}
            actionButton={
                <Button
                    className={styles.paymentButton}
                    size="large"
                    variant="primary"
                    fullWidth
                    onClick={onPay}>
                    Оплатить
                </Button>
            }
        />
    )
}