import {formatPrice} from "@/shared/lib";
import styles from "./CartSummary.module.css";
import {useCartTotals} from "@/entities/cart";
import {Button} from "@/shared/ui/button";
import {useCartSelectionContext} from "@/features/cart-selection";
import {useOrderActions} from "@/entities/order/model/useOrderActions.ts";
import {useCurrentPickupPoint} from "@/entities/pickup-point";
import {useNavigate} from "react-router-dom";
import {useSetActiveCheckoutOrder} from "@/entities/order";


export const CartSummary = () => {
    const {selectedPositions} = useCartSelectionContext();
    const goodsQuantity = selectedPositions.length;

    const {totalBasePrice, totalDiscountAmount, totalDiscountedPrice} = useCartTotals(selectedPositions);
    const {makeOrder} = useOrderActions();
    const setActiveOrder = useSetActiveCheckoutOrder();

    const currentPickupPoint = useCurrentPickupPoint();

    const navigate = useNavigate();


    const handleCheckout = async () => {
        const productIds: number[] = selectedPositions.map(cp => cp.productId);
        const pickupPointId: number = currentPickupPoint?.id ?? 1;
        const createdOrder = await makeOrder(productIds, pickupPointId);
        setActiveOrder(createdOrder);

        navigate('/checkout');
    };


    return (
        <section className={styles.cartSummary}>

            <Button
                className={styles.checkoutButton}
                size={"large"}
                variant={"primary"}
                fullWidth
                onClick={handleCheckout}>
                Перейти к оформлению
            </Button>

            <div className={styles.summaryDetails}>
                <div className={styles.summaryTitleRow}>
                    <h3 className={styles.summaryTitle}>
                        Ваша корзина
                    </h3>
                    <span className={styles.summaryCountBadge}>
                            {goodsQuantity}
                        </span>
                </div>

                <div className={styles.summaryRow}>
                    <span>Товары ({goodsQuantity})</span>
                    <span className={styles.oldPriceSum}>
                            {formatPrice(totalBasePrice)} ₽
                        </span>
                </div>

                {totalDiscountAmount > 0 && (
                    <div className={`${styles.summaryRow} ${styles.discountRow}`}>
                            <span>
                                Скидка
                            </span>
                        <span>
                                -{formatPrice(totalDiscountAmount)} ₽
                            </span>
                    </div>
                )}

                <div className={styles.totalBlock}>
                    <div className={styles.totalRow}>
                        <span className={styles.totalLabel}>Итого</span>
                        <span className={styles.totalPrice}>{formatPrice(totalDiscountedPrice)} ₽</span>
                    </div>
                </div>
            </div>
        </section>
    );
}