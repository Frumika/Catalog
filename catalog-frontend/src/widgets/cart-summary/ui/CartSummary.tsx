import {formatPrice} from "@/shared/lib";
import styles from "./CartSummary.module.css";
import {useCartTotals, type CartPosition} from "@/entities/cart";
import {Button} from "@/shared/ui/button";



interface CartSummaryProps {
    cartPositions: CartPosition[];
    totalQuantity: number;
}

export const CartSummary = (
    {
        cartPositions,
        totalQuantity,
    } : CartSummaryProps
) => {
    const {totalBasePrice, totalDiscountAmount, totalDiscountedPrice} = useCartTotals(cartPositions);

    return (
        <section className={styles.cartSummary}>

            <Button
                className={styles.checkoutButton}
                size={"large"}
                variant={"primary"}
                fullWidth>
                Перейти к оформлению
            </Button>

            <div className={styles.summaryDetails}>
                <div className={styles.summaryTitleRow}>
                    <h3 className={styles.summaryTitle}>
                        Ваша корзина
                    </h3>
                    <span className={styles.summaryCountBadge}>
                            {totalQuantity}
                        </span>
                </div>

                <div className={styles.summaryRow}>
                    <span>Товары ({totalQuantity})</span>
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