import {NavButton} from "@/shared/ui/nav-button";
import OrderIcon from "@/shared/assets/icons/order.svg?react";
import WishIcon from "@/shared/assets/icons/wish.svg?react";

import styles from "./NavGroup.module.css";
import type {ComponentDisplayMode} from "@/shared/lib";
import {ProfileButton} from "@/features/profile-button";
import {CartButton} from "@/features/cart-button";


interface NavGroupProps {
    displayMode?: ComponentDisplayMode;
}

export const NavGroup = (
    {
        displayMode = "full"
    }: NavGroupProps) => {

    return (
        <div className={styles.navGroup}>
            <ProfileButton displayMode={displayMode}/>

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

            <CartButton displayMode={displayMode}/>
        </div>
    );
};