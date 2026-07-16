import {NavButton} from "@/shared/ui/nav-button";
import CartIcon from "@/shared/assets/icons/cart.svg?react";
import type {ButtonHTMLAttributes} from "react";
import type {ComponentDisplayMode} from "@/shared/lib";
import {useNavigate} from "react-router-dom";
import {useCartTotalQuantity} from "@/entities/cart";


interface CartButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    displayMode?: ComponentDisplayMode;
}

export const CartButton = (
    {
        displayMode = "full",
        className,
        ...props
    }: CartButtonProps
) => {

    const navigate = useNavigate();
    const totalQuantity = useCartTotalQuantity();

    return (
        <NavButton
            {...props}
            displayMode={displayMode}
            icon={<CartIcon/>}
            badgeValue={totalQuantity}
            onClick={() => navigate("/cart")}
        >
            Корзина
        </NavButton>
    );
}