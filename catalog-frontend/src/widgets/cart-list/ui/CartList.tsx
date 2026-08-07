import styles from "./CartList.module.css";
import {type CartPosition, CartPositionCard} from "@/entities/cart";
import {ToggleWishedButton} from "@/features/toggle-wished";


interface CartListProps {
    cartPositions: CartPosition[];
    isSelected: (cartPosition: CartPosition) => boolean;
    onTogglePosition: (cartPosition: CartPosition) => void;
}

export const CartList = (
    {
        cartPositions,
        isSelected,
        onTogglePosition,
    }: CartListProps
) => {
    return (
        <section className={styles.cartList}>
            {cartPositions.map((cartPosition: CartPosition) => (
                <CartPositionCard
                    key={cartPosition.productId}
                    cartPosition={cartPosition}
                    isSelected={isSelected(cartPosition)}
                    onTogglePosition={onTogglePosition}
                    wishButtonSlot={
                        <ToggleWishedButton productId={cartPosition.productId}
                                            buttonType={"cartPosition"}/>
                    }
                />
            ))}
        </section>
    );
}

