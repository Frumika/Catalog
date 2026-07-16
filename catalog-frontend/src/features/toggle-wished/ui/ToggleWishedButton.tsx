import styles from "./ToggleWishedButton.module.css";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import {Icon} from "@/shared/ui/icon";
import {useIsProductWished, useWishlistActions} from "@/entities/wishlist";


interface ToggleWishedButtonProps {
    productId: number;
    className?: string;
}

export const ToggleWishedButton = (
    {
        productId,
        className,
    }: ToggleWishedButtonProps
) => {

    const isWished = useIsProductWished(productId);
    const {addProduct, removeProduct} = useWishlistActions();

    const toggleWishedButtonStyles = [
        styles.toggleButton,
        className,
    ].filter(Boolean).join(' ');

    return (
        <button
            className={toggleWishedButtonStyles}
            onClick={(event) => {
                event.stopPropagation();
                isWished ? removeProduct(productId) : addProduct(productId);
            }}
        >
            <Icon className={isWished ? styles.wished : styles.normal}>
                <WishIcon/>
            </Icon>
        </button>
    );
}