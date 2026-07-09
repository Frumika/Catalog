import { useNavigate } from "react-router-dom";
import {NavButton} from "@/shared/ui/nav-button";
import OrderIcon from "@/shared/assets/icons/order.svg?react";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import CartIcon from "@/shared/assets/icons/cart.svg?react";
import styles from "./NavGroup.module.css";
import type {ComponentDisplayMode} from "@/shared/lib";
import {ProfileButton} from "@/features/profile-button";


interface NavGroupProps {
    displayMode?: ComponentDisplayMode;
}

export const NavGroup = (
    {
        displayMode = "full"
    }: NavGroupProps) => {
    const navigate = useNavigate();

    return (
        <div className={styles.navGroup}>
            <ProfileButton>
                {"User_sk19dsdfsfsd"}
            </ProfileButton>

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
                onClick={() => navigate("/cart")}
            >
                Корзина
            </NavButton>
        </div>
    );
};