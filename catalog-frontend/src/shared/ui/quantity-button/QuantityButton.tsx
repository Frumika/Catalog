import type {ComponentSize} from "@/shared/lib";
import {Button} from "@/shared/ui/button";
import styles from "./QuantityButton.module.css"
import MinusIcon from "@/shared/assets/icons/minus.svg?react";
import PlusIcon from "@/shared/assets/icons/plus.svg?react";


type QuantityButtonVariant = 'secondary' | 'neutral';

interface QuantityButtonProps {
    size?: ComponentSize;
    variant?: QuantityButtonVariant;
    quantity: number;
    incQuantity: () => void;
    decQuantity: () => void;
    className?: string;
}

export const QuantityButton = (
    {
        size = 'small',
        variant = 'secondary',
        quantity,
        incQuantity,
        decQuantity,
        className,
    }: QuantityButtonProps
) => {
    const quantityButtonStyles = [
        styles.quantityButton,
        styles[variant],
        className
    ].filter(Boolean).join(' ');

    return (
        <div className={quantityButtonStyles}>
            <Button
                className={styles.button}
                variant={variant}
                size={size}
                icon={<MinusIcon className={styles.icon}/>}
                onClick={decQuantity}
            />

            <span className={styles.quantity}>
                {quantity}
            </span>

            <Button
                className={styles.button}
                variant={variant}
                size={size}
                icon={<PlusIcon className={styles.icon}/>}
                onClick={incQuantity}
            />
        </div>
    );
}