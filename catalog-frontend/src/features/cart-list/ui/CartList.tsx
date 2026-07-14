import styles from "./CartList.module.css";
import type {CartPosition} from "@/entities/cart/model/types.ts";
import {CartPositionCard} from "@/entities/cart";

interface CartListProps {
    cartPositions: CartPosition[];
}

export const CartList = (
    {
        cartPositions,
    }: CartListProps
) => {

    return (
        <div className={styles.cartContainer}>
            <div className={styles.mainContent}>
                <div className={styles.listHeader}>
                    <h2 className={styles.title}>
                        Корзина <span className={styles.count}>{cartPositions.length}</span>
                    </h2>
                </div>

                <div className={styles.list}>
                    {cartPositions.map((item) => (
                        <CartPositionCard
                            key={item.productId}
                            cartPosition={item}/>
                    ))}
                </div>
            </div>

            {/*<aside className={styles.sidebar}>*/}
            {/*    <div className={styles.summaryCard}>*/}
            {/*        <button className={styles.mainCheckoutBtn} disabled={=== 0}>*/}
            {/*            Перейти к оформлению*/}
            {/*        </button>*/}

            {/*        <div className={styles.summaryDetails}>*/}
            {/*            <div className={styles.summaryTitleRow}>*/}
            {/*                <h3 className={styles.summaryTitle}>Ваша корзина</h3>*/}
            {/*                <span className={styles.summaryCountBadge}>*/}
            {/*                    {clientTotalQuantity} {getProductWord(clientTotalQuantity)}*/}
            {/*                </span>*/}
            {/*            </div>*/}

            {/*            <div className={styles.summaryRow}>*/}
            {/*                <span>Товары ({clientTotalQuantity})</span>*/}
            {/*                <span className={styles.oldPriceSum}>{clientBasePrice.toLocaleString()} ₽</span>*/}
            {/*            </div>*/}

            {/*            {clientDiscount > 0 && (*/}
            {/*                <div className={`${styles.summaryRow} ${styles.discountRow}`}>*/}
            {/*                    <span>Скидка</span>*/}
            {/*                    <span>-{clientDiscount.toLocaleString()} ₽</span>*/}
            {/*                </div>*/}
            {/*            )}*/}

            {/*            <div className={styles.totalBlock}>*/}
            {/*                <div className={styles.totalRow}>*/}
            {/*                    <span className={styles.totalLabel}>Итого</span>*/}
            {/*                    <span className={styles.totalPrice}>{clientFinalPrice.toLocaleString()} ₽</span>*/}
            {/*                </div>*/}
            {/*            </div>*/}
            {/*        </div>*/}
            {/*    </div>*/}
            {/*</aside>*/}
        </div>
    );
}