import type {CartPosition} from "../model/types.ts";
import styles from "./CartItem.module.css";

interface CartItemProps {
    cartPosition: CartPosition;
}

export const CartItem = ({ cartPosition }: CartItemProps) => {
    return (
        <div className={styles.item}>
            {/* Левая часть: Чекбокс и Изображение */}
            <div className={styles.leftContainer}>
                <label className={styles.checkboxWrapper}>
                    <input type="checkbox" defaultChecked className={styles.checkbox} />
                    <span className={styles.customCheckbox}></span>
                </label>

                <div className={styles.imageWrapper}>
                    <img
                        src={cartPosition.imageUrl}
                        alt={cartPosition.productName}
                        className={styles.image}
                    />
                </div>
            </div>

            {/* Правая часть: Информация, счетчик и цены */}
            <div className={styles.content}>
                <div className={styles.info}>
                    <h3 className={styles.title}>{cartPosition.productName}</h3>

                    {/* Нижняя панель действий (Удалить / В избранное) */}
                    <div className={styles.metaActions}>
                        <button className={styles.metaButton}>В избранное</button>
                        <button className={styles.metaButton}>Удалить</button>
                    </div>
                </div>

                {/* Блок управления количеством (Счетчик) */}
                <div className={styles.quantityControls}>
                    <div className={styles.counter}>
                        <button className={styles.counterBtn} disabled={cartPosition.reviewCount <= 1}>–</button>
                        <span className={styles.quantityNum}>{cartPosition.reviewCount}</span>
                        <button className={styles.counterBtn}>+</button>
                    </div>
                </div>

                {/* Блок стоимости */}
                <div className={styles.priceBlock}>
                    <span className={styles.currentPrice}>
                        {cartPosition.price.toLocaleString()} ₽
                    </span>
                </div>
            </div>
        </div>
    );
};