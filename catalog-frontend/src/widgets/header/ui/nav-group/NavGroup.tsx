import { useNavigate } from "react-router-dom";
import {NavButton} from "@/shared/ui/nav-button";
import OrderIcon from "@/shared/assets/icons/order.svg?react";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import CartIcon from "@/shared/assets/icons/cart.svg?react";
import styles from "./NavGroup.module.css";
import type {ComponentDisplayMode} from "@/shared/lib";
import {ProfileButton} from "@/features/profile-button";
import { useCartStore } from "@/entities/cart";


interface NavGroupProps {
    displayMode?: ComponentDisplayMode;
}

export const NavGroup = (
    {
        displayMode = "full"
    }: NavGroupProps) => {
    const navigate = useNavigate();
    const cartItems = useCartStore(state => state.items);
    const totalCartQuantity = cartItems.reduce((sum, item) => sum + item.quantity, 0);

    return (
        <div className={styles.navGroup}>
            <ProfileButton/>

            <NavButton
                displayMode={displayMode}
                icon={<OrderIcon/>}
                badgeValue={150}
            >
                Заказы
            </NavButton>

            <NavButton
                displayMode={displayMode}
                icon={<WishIcon/>}
                badgeValue={5}
            >
                Избранное
            </NavButton>

            <NavButton
                displayMode={displayMode}
                icon={<CartIcon/>}
                badgeValue={totalCartQuantity}
                onClick={() => navigate("/cart")}

            >
                Корзина
            </NavButton>
        </div>
    );
};