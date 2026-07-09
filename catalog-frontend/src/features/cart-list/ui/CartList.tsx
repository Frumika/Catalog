// features/cart-list/ui/CartList.tsx
import { useEffect, useState } from "react";
import { CartItem } from "@/entities/cart-item";
import { cartApi } from "@/entities/cart-item/api/cartApi";
import type { CartPosition } from "@/entities/cart-item/model/types";
import styles from "./CartList.module.css";

// Расширяем интерфейс локально: добавляем флаг checked, которого нет на сервере
interface CheckedCartPosition extends CartPosition {
    checked: boolean;
}

export const CartList = () => {
    const [cartData, setCartData] = useState<{
        items: CheckedCartPosition[];
        totalQuantity: number;
        finalPrice: number;
    } | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        cartApi.getCartPositions()
            .then((data) => {
                // Мапим данные с сервера и каждому элементу ОДНОКРАТНО добавляем checked: true
                const itemsWithCheck = data.items.map((item) => ({
                    ...item,
                    checked: true
                }));

                setCartData({
                    ...data,
                    items: itemsWithCheck
                });
            })
            .catch((err) => console.error("Ошибка загрузки корзины:", err))
            .finally(() => setLoading(false));
    }, []);

    // 1. Переключение чекбокса (работает локально на фронтенде)
    const handleToggleCheck = (productId: number) => {
        if (!cartData) return;
        setCartData(prev => {
            if (!prev) return null;
            return {
                ...prev,
                items: prev.items.map(item =>
                    item.productId === productId ? { ...item, checked: !item.checked } : item
                )
            };
        });
    };

    // Изменение количества
    const handleUpdateQuantity = async (productId: number, newCount: number) => {
        if (newCount < 1 || !cartData) return;

        setCartData(prev => {
            if (!prev) return null;
            return {
                ...prev,
                items: prev.items.map(item =>
                    item.productId === productId ? { ...item, quantity: newCount } : item
                )
            };
        });

        try {
            // Передаем number напрямую, как просит бэкенд
            await cartApi.updateQuantity(productId, newCount);
        } catch (err) {
            console.error("Ошибка при обновлении количества на сервере:", err);
        }
    };

// Удаление товара
    const handleRemoveItem = async (productId: number) => {
        if (!cartData) return;

        setCartData(prev => {
            if (!prev) return null;
            return {
                ...prev,
                items: prev.items.filter(item => item.productId !== productId)
            };
        });

        try {
            // Передаем number напрямую
            await cartApi.removeItem(productId);
        } catch (err) {
            console.error("Ошибка удаления товара на сервере:", err);
        }
    };

    if (loading) return <div>Загрузка корзины...</div>;
    if (!cartData || cartData.items.length === 0) return <div>Корзина пуста</div>;

    // Фильтруем только те элементы, у которых стоит галочка
    const activeItems = cartData.items.filter(item => item.checked);

    // Пересчитываем итоги на лету на основе ваших полей DTO
    const clientTotalQuantity = activeItems.reduce((sum, item) => sum + item.quantity, 0);
    const clientFinalPrice = activeItems.reduce((sum, item) => sum + (item.priceWithDiscount * item.quantity), 0);

    return (
        <div className={styles.cartContainer}>
            <div className={styles.mainContent}>
                <div className={styles.listHeader}>
                    <h2 className={styles.title}>
                        Корзина <span className={styles.count}>{cartData.items.length}</span>
                    </h2>
                </div>

                <div className={styles.list}>
                    {cartData.items.map((item) => (
                        <CartItem
                            key={item.productId}
                            cartPosition={item}
                            isChecked={item.checked} // Передаем состояние чекбокса
                            onToggleCheck={() => handleToggleCheck(item.productId)}
                            onIncrement={() => handleUpdateQuantity(item.productId, item.quantity + 1)}
                            onDecrement={() => handleUpdateQuantity(item.productId, item.quantity - 1)}
                            onRemove={() => handleRemoveItem(item.productId)}
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
                        <h3 className={styles.summaryTitle}>Ваша корзина</h3>

                        <div className={styles.summaryRow}>
                            <span>Товары ({clientTotalQuantity})</span>
                            <span>{clientFinalPrice.toLocaleString()} ₽</span>
                        </div>

                        <hr className={styles.divider} />

                        <div className={styles.totalRow}>
                            <span>Итого</span>
                            <span className={styles.totalPrice}>
                                {clientFinalPrice.toLocaleString()} ₽
                            </span>
                        </div>
                    </div>
                </div>
            </aside>
        </div>
    );
};