import styles from "./CartSummary.module.css";
import {Button} from "@/shared/ui/button";
import {useCartSelectionContext} from "@/features/cart-selection";
import {useOrderActions} from "@/entities/order/model/useOrderActions.ts";
import {useCurrentPickupPoint} from "@/entities/pickup-point";
import {useNavigate} from "react-router-dom";
import {useSetActiveCheckoutOrder} from "@/entities/order";
import {Summary} from "@/shared/ui/summary";


export const CartSummary = () => {
    const {selectedPositions} = useCartSelectionContext();
    const {makeOrder} = useOrderActions();
    const setActiveOrder = useSetActiveCheckoutOrder();
    const currentPickupPoint = useCurrentPickupPoint();
    const navigate = useNavigate();

    const handleCheckout = async () => {
        if (currentPickupPoint === null) return;

        const productIds: number[] = selectedPositions.map(cp => cp.productId);
        const pickupPointId: number = currentPickupPoint.id;
        const createdOrder = await makeOrder(productIds, pickupPointId);
        setActiveOrder(createdOrder);

        navigate('/checkout');
    };


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
                    onClick={handleCheckout}>
                    Перейти к оформлению
                </Button>
            }
        />
    );
}