import styles from "./CartSummary.module.css";
import {Button} from "@/shared/ui/button";
import {useCartSelectionContext} from "@/features/cart-selection";
import {Summary} from "@/shared/ui/summary";
import {useNotify} from "@/shared/lib";


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
    const notify = useNotify();

    const isCartEmpty = selectedPositions.length === 0;

    const handleCheckout =
        isCartEmpty ? () => notify("warning", "Сначала выберите товары") : onCheckout;

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
                    onClick={() => handleCheckout(productIds)}>
                    Перейти к оформлению
                </Button>
            }
        />
    );
}