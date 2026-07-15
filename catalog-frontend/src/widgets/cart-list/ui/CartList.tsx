import styles from "./CartList.module.css";
import {CartPositionCard, useCartPage} from "@/entities/cart";
import {useIsAuthenticated} from "@/entities/session";


export const CartList = () => {
    const isAuthenticated = useIsAuthenticated();
    const {cartPositions} = useCartPage(isAuthenticated);

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