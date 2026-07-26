import styles from "./CartList.module.css";
import {type CartPosition, CartPositionCard} from "@/entities/cart";
import {ToggleWishedButton} from "@/features/toggle-wished";


interface CartListProps {
    cartPositions: CartPosition[];
}

export const CartList = (
    {
        cartPositions,
    }: CartListProps
) => (
    <section className={styles.cartList}>
        {cartPositions.map((item) => (
            <CartPositionCard
                key={item.productId}
                cartPosition={item}
                wishButtonSlot={<ToggleWishedButton productId={item.productId} buttonType={"cartPosition"}/>}
            />
        ))}
    </section>
);
