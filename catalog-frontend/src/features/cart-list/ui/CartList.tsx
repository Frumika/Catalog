// features/cart-list/ui/CartList.tsx
import { useEffect } from "react";
import { CartItem } from "@/entities/cart-item";
import { useCartStore } from "@/entities/cart-item";
import styles from "./CartList.module.css";

export const CartList = () => {
    // Достаем toggleCheck из стора
    const {items, loading, updateQuantity, removeItem, toggleCheck} = useCartStore();

    if (loading && items.length === 0) return <div>Загрузка корзины...</div>;
    if (items.length === 0) return <div>Корзина пуста</div>;

    // ФИЛЬТРУЕМ товары: берем для финального просчета ТОЛЬКО выбранные (checked === true)
    // features/cart-list/ui/CartList.tsx
// ... (остальные импорты и логика хуков без изменений)

    const activeItems = items.filter(item => item.checked);

    // Считаем суммы на основе DTO полей
    const clientTotalQuantity = activeItems.reduce((sum, item) => sum + item.quantity, 0);
    const clientBasePrice = activeItems.reduce((sum, item) => sum + (item.basePrice * item.quantity), 0);
    const clientFinalPrice = activeItems.reduce((sum, item) => sum + (item.priceWithDiscount * item.quantity), 0);
    const clientDiscount = clientBasePrice - clientFinalPrice;

    // Склонение слова "товар" для шапки
    const getProductWord = (count: number) => {
        const lastDigit = count % 10;
        const lastTwoDigits = count % 100;
        if (lastTwoDigits >= 11 && lastTwoDigits <= 14) return "товаров";
        if (lastDigit === 1) return "товар";
        if (lastDigit >= 2 && lastDigit <= 4) return "товара";
        return "товаров";
    };

    return (
        <div className={styles.cartContainer}>
            <div className={styles.mainContent}>
                <div className={styles.listHeader}>
                    <h2 className={styles.title}>
                        Корзина <span className={styles.count}>{items.length}</span>
                    </h2>
                </div>

                <div className={styles.list}>
                    {items.map((item) => (
                        <CartItem
                            key={item.productId}
                            cartPosition={item}
                            isChecked={item.checked}
                            onToggleCheck={() => toggleCheck(item.productId)}
                            onIncrement={() => updateQuantity(item.productId, item.quantity, 'increment')}
                            onDecrement={() => updateQuantity(item.productId, item.quantity, 'decrement')}
                            onRemove={() => removeItem(item.productId)}
                        />
                    ))}
                </div>
            </div>

            <aside className={styles.sidebar}>
                <div className={styles.summaryCard}>
                    <button className={styles.mainCheckoutBtn} disabled={clientTotalQuantity === 0}>
                        Перейти к оформлению
                    </button>

                    <div className={styles.summaryDetails}>
                        <div className={styles.summaryTitleRow}>
                            <h3 className={styles.summaryTitle}>Ваша корзина</h3>
                            <span className={styles.summaryCountBadge}>
                                {clientTotalQuantity} {getProductWord(clientTotalQuantity)}
                            </span>
                        </div>

                        <div className={styles.summaryRow}>
                            <span>Товары ({clientTotalQuantity})</span>
                            <span className={styles.oldPriceSum}>{clientBasePrice.toLocaleString()} ₽</span>
                        </div>

                        {clientDiscount > 0 && (
                            <div className={`${styles.summaryRow} ${styles.discountRow}`}>
                                <span>Скидка</span>
                                <span>-{clientDiscount.toLocaleString()} ₽</span>
                            </div>
                        )}

                        <div className={styles.totalBlock}>
                            <div className={styles.totalRow}>
                                <span className={styles.totalLabel}>Итого</span>
                                <span className={styles.totalPrice}>{clientFinalPrice.toLocaleString()} ₽</span>
                            </div>
                        </div>
                    </div>
                </div>
            </aside>
        </div>
    );
}