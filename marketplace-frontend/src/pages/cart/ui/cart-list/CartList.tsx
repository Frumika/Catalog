import styles from "./CartList.module.css";
import {type CartPosition, CartPositionCard, useCartActions} from "@/entities/cart";
import {ToggleWishedButton} from "@/features/toggle-wished";
import {Checkbox} from "@/shared/ui/checkbox";
import {Button} from "@/shared/ui/button";
import TrashcanIcon from "@/shared/assets/icons/trashcan.svg?react";
import {useCartSelectionContext} from "@/features/cart-selection";


interface CartListProps {
    cartPositions: CartPosition[];
    onCheckout: (productIds: number[]) => void;
}

export const CartList = (
    {
        cartPositions,
        onCheckout,
    }: CartListProps
) => {
    const {clearCart} = useCartActions();
    const {isAllSelected, toggleAll, togglePosition, isSelected} = useCartSelectionContext();


    return (
        <section className={styles.cartList}>
            <div className={styles.common}>
                <span className={styles.commonLeft}>
                    <Checkbox selected={isAllSelected} onChange={toggleAll}/>
                    <p className={styles.text}>{"Выбрать все"}</p>
                </span>

                <Button size={"small"}
                        icon={<TrashcanIcon/>}
                        variant={"neutral"}
                        onClick={clearCart}>
                    Очистить корзину
                </Button>
            </div>

            <div className={styles.list}>
                {cartPositions.map((cartPosition: CartPosition) => (
                    <CartPositionCard
                        key={cartPosition.productId}
                        cartPosition={cartPosition}
                        isSelected={isSelected(cartPosition)}
                        onTogglePosition={togglePosition}
                        wishButtonSlot={
                            <ToggleWishedButton productId={cartPosition.productId}
                                                buttonType={"cartPosition"}/>
                        }
                        onCheckout={onCheckout}
                    />
                ))}
            </div>
        </section>
    );
}

