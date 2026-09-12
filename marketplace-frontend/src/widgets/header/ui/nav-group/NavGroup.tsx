import styles from "./NavGroup.module.css";
import OrderIcon from "@/shared/assets/icons/order.svg?react";
import {NavButton} from "@/shared/ui/nav-button";
import {CartButton} from "../cart-button/CartButton.tsx";
import {ProfileButton} from "@/features/auth";
import {WishlistButton} from "../wishlist-button/WishlistButton.tsx";
import type {ComponentDisplayMode} from "@/shared/model";
import {useNotify} from "@/shared/lib/notification";


interface NavGroupProps {
    displayMode?: ComponentDisplayMode;
}

export const NavGroup = (
    {
        displayMode = "full"
    }: NavGroupProps) => {

    const notify = useNotify();

    return (
        <div className={styles.navGroup}>

            <ProfileButton displayMode={displayMode}/>

            <NavButton
                displayMode={displayMode}
                icon={<OrderIcon/>}
                badgeValue={0}
                onClick={() => notify("warning", "Заказы пока не реализованы")}>
                Заказы
            </NavButton>

            <WishlistButton displayMode={displayMode}/>

            <CartButton displayMode={displayMode}/>
        </div>
    );
};