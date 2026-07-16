import styles from "./NavGroup.module.css";
import OrderIcon from "@/shared/assets/icons/order.svg?react";
import {NavButton} from "@/shared/ui/nav-button";
import type {ComponentDisplayMode} from "@/shared/lib";
import {CartButton} from "../cart-button/CartButton.tsx";
import {ProfileButton} from "../profile-button/ProfileButton.tsx";
import {WishlistButton} from "../wishlist-button/WishlistButton.tsx";


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

            <WishlistButton displayMode={displayMode}/>

            <CartButton displayMode={displayMode}/>
        </div>
    );
};