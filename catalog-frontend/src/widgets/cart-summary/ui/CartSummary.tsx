import styles from "./CartSummary.module.css";
import {Button} from "@/shared/ui/button";
import {useCartSelectionContext} from "@/features/cart-selection";
import {Summary} from "@/shared/ui/summary";


interface CartSummaryProps {
    onCheckout: (productIds: number[]) => void;
}

export const CartSummary = (
    {
        onCheckout,
    }: CartSummaryProps
) => {
    const {selectedPositions} = useCartSelectionContext();
    const productIds = selectedPositions.map(cp => cp.productId);

    return (
        <Summary
            title="Ваша корзина"
            positions={selectedPositions}
            actionButton={
                <Button
                    className={styles.checkoutButton}
                    size={"large"}
                    variant={"primary"}
                    fullWidth
                    onClick={() => {
                        onCheckout(productIds);
                    }}>
                    Перейти к оформлению
                </Button>
            }
        />
    );
}