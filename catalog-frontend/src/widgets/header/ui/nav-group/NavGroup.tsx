import {NavButton} from "@/shared/ui/nav-button";
import ProfileIcon from "@/shared/assets/icons/profile.svg?react";
import OrderIcon from "@/shared/assets/icons/order.svg?react";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import CartIcon from "@/shared/assets/icons/cart.svg?react";
import styles from "./NavGroup.module.css";
import type {ComponentDisplayMode} from "@/shared/lib";


interface NavGroupProps {
    displayMode?: ComponentDisplayMode;
}

export const NavGroup = (
    {
        displayMode = "full"
    }: NavGroupProps) => {

    return (
        <div className={styles.navGroup}>
            <NavButton
                displayMode={displayMode}
                icon={<ProfileIcon/>}
            >
                Войти
            </NavButton>

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
                badgeValue={10}
            >
                Корзина
            </NavButton>
        </div>
    );
};