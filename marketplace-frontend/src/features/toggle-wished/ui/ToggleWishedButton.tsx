import styles from "./ToggleWishedButton.module.css";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import {Icon} from "@/shared/ui/icon";
import {useIsProductWished, useWishlistActions} from "@/entities/wishlist";
import {Button} from "@/shared/ui/button";


type ToggleWishedButtonType = 'productCard' | 'cartPosition';

interface ToggleWishedButtonProps {
    productId: number;
    buttonType?: ToggleWishedButtonType;
    className?: string;
}

export const ToggleWishedButton = (
    {
        productId,
        buttonType = "productCard",
        className,
    }: ToggleWishedButtonProps) => {

    const isWished = useIsProductWished(productId);
    const {addProduct, removeProduct} = useWishlistActions();

    const handleClick = (event: React.MouseEvent) => {
        event.stopPropagation();
        isWished ? removeProduct(productId) : addProduct(productId);
    };

    if (buttonType === 'cartPosition') {
        return (
            <Button
                className={[isWished ? styles.wishButton : undefined, className].filter(Boolean).join(' ')}
                variant="neutral"
                icon={<WishIcon/>}
                size="small"
                onClick={handleClick}
            />
        );
    }

    return (
        <button
            className={[styles.productCard, className].filter(Boolean).join(' ')}
            onClick={handleClick}
            data-wished={isWished}
        >
            <Icon className={styles.icon}>
                <WishIcon/>
            </Icon>
        </button>
    );
};