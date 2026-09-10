import styles from "./CheckoutSummary.module.css";
import {Button} from "@/shared/ui/button";
import type {PricedPosition} from "@/shared/types";
import {Summary} from "@/shared/ui/summary";


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