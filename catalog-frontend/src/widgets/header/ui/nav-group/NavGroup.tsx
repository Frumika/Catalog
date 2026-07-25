import styles from "./NavGroup.module.css";
import OrderIcon from "@/shared/assets/icons/order.svg?react";
import {NavButton} from "@/shared/ui/nav-button";
import type {ComponentDisplayMode} from "@/shared/lib";
import {CartButton} from "../cart-button/CartButton.tsx";
import {ProfileButton} from "@/features/auth";
import {WishlistButton} from "../wishlist-button/WishlistButton.tsx";
import {ProfilePopover} from "@/widgets/header/ui/profile-popover/ProfilePopover.tsx";
import {useRef} from "react";
import {useProfilePopover} from "@/widgets/header/model/useProfilePopover.ts";


interface NavGroupProps {
    displayMode?: ComponentDisplayMode;
}

export const NavGroup = (
    {
        displayMode = "full"
    }: NavGroupProps) => {

    const anchorRef = useRef<HTMLButtonElement>(null);
    const {isOpen, open, close} = useProfilePopover();

    return (
        <div className={styles.navGroup}>
            <ProfileButton
                ref={anchorRef}
                displayMode={displayMode}
                onClick={open}
            />

            <ProfilePopover
                isOpen={isOpen}
                onClose={close}
                anchorRef={anchorRef}/>

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