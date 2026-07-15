import styles from "./CartList.module.css";
import {type CartPosition, CartPositionCard} from "@/entities/cart";


interface CartListProps {
    cartPositions: CartPosition[];
}

export const CartList = (
    {
        cartPositions,
    }: CartListProps
) => {
    return (
        <section className={styles.cartList}>
            {cartPositions.map((item) => (
                <CartPositionCard
                    key={item.productId}
                    cartPosition={item}/>
            ))}
        </section>
    );
}