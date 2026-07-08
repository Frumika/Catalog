import type { ProductPreview } from "@/entities/product/model/Product.types";
import { CartItem } from "@/entities/cart-item";
import styles from "./Cart.module.css";

interface CartProps {
    products: ProductPreview[];
}

export const CartList = ({ products }: CartProps) => {
    const totalPrice = products.reduce(
        (sum, product) => sum + product.price,
        0
    );

    return (
        <div className={styles.cart}>
            <div className={styles.content}>
                <div className={styles.left}>
                    <div className={styles.header}>
                        <h1>Корзина</h1>
                        <span>{products.length}</span>
                    </div>

                    <div className={styles.list}>
                        {products.map((product) => (
                            <CartItem
                                key={product.productId}
                                product={product}
                            />
                        ))}
                    </div>
                </div>

                <aside className={styles.sidebar}>
                    <div className={styles.summary}>
                        <h2>Ваш заказ</h2>

                        <div className={styles.row}>
                            <span>Товаров</span>
                            <span>{products.length}</span>
                        </div>

                        <div className={styles.row}>
                            <span>Итого</span>
                            <span>{totalPrice.toLocaleString()} ₽</span>
                        </div>

                        <button className={styles.checkout}>
                            Перейти к оформлению
                        </button>
                    </div>
                </aside>
            </div>
        </div>
    );
};