import type { ProductPreview } from "../model/Product.types.ts";
import styles from "./CartItem.module.css";

interface CartItemProps {
    product: ProductPreview;
}

export const CartItem = ({ product }: CartItemProps) => {
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
                        src={product.imageUrl}
                        alt={product.productName}
                        className={styles.image}
                    />
                </div>
            </div>

            {/* Правая часть: Информация, счетчик и цены */}
            <div className={styles.content}>
                <div className={styles.info}>
                    <h3 className={styles.title}>{product.productName}</h3>

                    {/* Нижняя панель действий (Удалить / В избранное) */}
                    <div className={styles.metaActions}>
                        <button className={styles.metaButton}>В избранное</button>
                        <button className={styles.metaButton}>Удалить</button>
                    </div>
                </div>

                {/* Блок управления количеством (Счетчик) */}
                <div className={styles.quantityControls}>
                    <div className={styles.counter}>
                        <button className={styles.counterBtn} disabled={product.reviewCount <= 1}>–</button>
                        <span className={styles.quantityNum}>{product.reviewCount}</span>
                        <button className={styles.counterBtn}>+</button>
                    </div>
                </div>

                {/* Блок стоимости */}
                <div className={styles.priceBlock}>
                    <span className={styles.currentPrice}>
                        {product.price.toLocaleString()} ₽
                    </span>
                </div>
            </div>
        </div>
    );
};