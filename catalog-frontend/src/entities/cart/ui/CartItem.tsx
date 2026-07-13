// entities/cart-item/ui/CartItem.tsx
import type {CartPosition} from "../model/types.ts";
import styles from "./CartItem.module.css";

interface CartItemProps {
    cartPosition: CartPosition;
    isChecked: boolean;
    onToggleCheck: () => void;
    onIncrement: () => void;
    onDecrement: () => void;
    onRemove: () => void;
}

// entities/cart-item/ui/CartItem.tsx
// ... (интерфейсы пропсов остаются прежними)

export const CartItem = (
    {
        cartPosition,
        isChecked,
        onToggleCheck,
        onIncrement,
        onDecrement,
        onRemove
    }: CartItemProps) => {
    const hasDiscount = cartPosition.discountPercent > 0;

    return (
        <div className={styles.item}>
            <div className={styles.leftContainer}>
                {/*<label className={styles.checkboxWrapper}>*/}
                {/*    <input type="checkbox" checked={isChecked} onChange={onToggleCheck} className={styles.checkbox} />*/}
                {/*    <span className={styles.customCheckbox}></span>*/}
                {/*</label>*/}

                <div className={styles.imageWrapper}>
                    <img src={cartPosition.imageUrl} alt="" className={styles.image}/>
                </div>
            </div>

            <div className={styles.content}>
                <div className={styles.info}>
                    <h3 className={styles.title}>{cartPosition.productName}</h3>
                    <div className={styles.metaActions}>
                        <button className={styles.metaButton}>
                            <span className={styles.heartIcon}>♡</span> В избранное
                        </button>
                        <button className={styles.metaButton} onClick={onRemove}>
                            🗑️ Удалить
                        </button>
                    </div>
                </div>


                <div className={styles.priceBlock}>
                    <span className={`${styles.currentPrice} ${hasDiscount ? styles.pinkPrice : ''}`}>
                        {(cartPosition.priceWithDiscount * cartPosition.quantity).toLocaleString()} ₽
                    </span>
                    {hasDiscount && (
                        <span className={styles.oldPrice}>
                            {(cartPosition.basePrice * cartPosition.quantity).toLocaleString()} ₽
                        </span>
                    )}
                </div>

                <div className={styles.quantityControls}>
                    <div className={styles.counterWrapper}>
                        <div className={styles.counter}>
                            <button className={styles.counterBtn} onClick={onDecrement}>–</button>
                            <span className={styles.quantityNum}>{cartPosition.quantity}</span>
                            <button className={styles.counterBtn} onClick={onIncrement}>+</button>
                        </div>
                        {cartPosition.quantity >= 2 && (
                            <span className={styles.counterWarning}>Количество ограничено</span>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};