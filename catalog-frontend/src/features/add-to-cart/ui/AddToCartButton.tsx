import {useCartActions, usePositionQuantity} from "@/entities/cart";
import {QuantityButton} from "@/shared/ui/quantity-button";
import {Button} from "@/shared/ui/button";
import CartIcon from "@/shared/assets/icons/cart.svg?react";
import type {ComponentSize} from "@/shared/lib";


interface AddToCartButtonProps {
    productId: number;
    size?: ComponentSize;
}

export const AddToCartButton = (
    {
        productId,
        size = "small"
    }: AddToCartButtonProps) => {

    const {addProduct, updateQuantity} = useCartActions();
    const quantity = usePositionQuantity(productId);

    return quantity > 0 ? (
        <QuantityButton
            size={size}
            variant="secondary"
            quantity={quantity}
            incQuantity={() => updateQuantity(productId, quantity + 1)}
            decQuantity={() => updateQuantity(productId, quantity - 1)}
        />
    ) : (
        <Button fullWidth size={size} icon={<CartIcon/>} onClick={() => addProduct(productId)}>
            В корзину
        </Button>
    );
};