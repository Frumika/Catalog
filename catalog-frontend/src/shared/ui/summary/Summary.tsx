import styles from "./Summary.module.css";
import type {PricedPosition} from "@/shared/types";
import type {ReactNode} from "react";
import {formatPrice, getPositionsTotals, getTotalQuantity} from "@/shared/lib";


interface SummaryProps {
    title: string;
    positions: PricedPosition[];
    actionButton: ReactNode;
    className?: string;
}

export const Summary = (
    {
        title,
        positions,
        actionButton,
        className,
    }: SummaryProps) => {

    const {totalBasePrice, totalDiscountAmount, totalDiscountedPrice} = getPositionsTotals(positions);
    const totalQuantity = getTotalQuantity(positions);

    return (
        <section className={`${styles.summary} ${className ?? ''}`}>
            {actionButton}

            <div className={styles.summaryDetails}>
                <div className={styles.summaryTitleRow}>
                    <h3 className={styles.summaryTitle}>{title}</h3>
                    <span className={styles.summaryCountBadge}>{totalQuantity}</span>
                </div>

                <div className={styles.summaryRow}>
                    <span>Товары ({totalQuantity})</span>
                    <span className={styles.oldPriceSum}>{formatPrice(totalBasePrice)} ₽</span>
                </div>

                {totalDiscountAmount > 0 && (
                    <div className={`${styles.summaryRow} ${styles.discountRow}`}>
                        <span>Скидка</span>
                        <span>-{formatPrice(totalDiscountAmount)} ₽</span>
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