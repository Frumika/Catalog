// features/cart-list/ui/CartList.tsx
import { useEffect } from "react";
import { CartItem } from "@/entities/cart-item";
import { useCartStore } from "@/entities/cart-item";
import styles from "./CartList.module.css";

export const CartList = () => {
    const { items, loading, updateQuantity } = useCartStore();

    if (loading && items.length === 0) return <div>Загрузка корзины...</div>;
    if (items.length === 0) return <div>Корзина пуста</div>;

    // Для простоты перерасчета стоимости на странице корзины:
    const clientTotalQuantity = items.reduce((sum, item) => sum + item.quantity, 0);
    const clientFinalPrice = items.reduce((sum, item) => sum + (item.priceWithDiscount * item.quantity), 0);

    return (
        <div className={styles.cartContainer}>
            <div className={styles.mainContent}>
                <div className={styles.listHeader}>
                    <h2 className={styles.title}>Корзина</h2>
                </div>

                <div className={styles.list}>
                    {items.map((item) => (
                        <CartItem
                            key={item.productId}
                            cartPosition={item}
                            isChecked={true} // Можно связать с локальным стейтом чеков, если нужно выборочно оформлять
                            onToggleCheck={() => {}}
                            onIncrement={() => updateQuantity(item.productId, item.quantity, 'increment')}
                            onDecrement={() => updateQuantity(item.productId, item.quantity, 'decrement')}
                            onRemove={() => updateQuantity(item.productId, item.quantity, 'decrement')} // уменьшение до нуля само вызовет удаление
                        />
                    ))}
                </div>
            </div>

            <aside className={styles.sidebar}>
                <div className={styles.summaryCard}>
                    <button className={styles.mainCheckoutBtn}>Перейти к оформлению</button>
                    <div className={styles.summaryDetails}>
                        <h3 className={styles.summaryTitle}>Ваша корзина</h3>
                        <div className={styles.summaryRow}>
                            <span>Товары ({clientTotalQuantity})</span>
                            <span>{clientFinalPrice.toLocaleString()} ₽</span>
                        </div>
                        <hr className={styles.divider} />
                        <div className={styles.totalRow}>
                            <span>Итого</span>
                            <span className={styles.totalPrice}>{clientFinalPrice.toLocaleString()} ₽</span>
                        </div>
                    </div>
                </div>
            </aside>
        </div>
    );
};