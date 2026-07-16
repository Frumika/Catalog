import type {ComponentDisplayMode} from "@/shared/lib";
import type {ButtonHTMLAttributes} from "react";
import {useNavigate} from "react-router-dom";
import {NavButton} from "@/shared/ui/nav-button";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import {useWishlistTotalQuantity} from "@/entities/wishlist/model/wishlistStore.ts";


interface WishlistButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    displayMode?: ComponentDisplayMode;
}

export const WishlistButton = (
    {
        displayMode = "full",
        className,
        ...props
    }: WishlistButtonProps
) => {
    const navigate = useNavigate();
    const wishlistTotalQuantity = useWishlistTotalQuantity();

    return (
        <NavButton
            {...props}
            displayMode={displayMode}
            icon={<WishIcon/>}
            badgeValue={wishlistTotalQuantity}
            onClick={() => {
            }}
        >
            Избранное
        </NavButton>
    );
}