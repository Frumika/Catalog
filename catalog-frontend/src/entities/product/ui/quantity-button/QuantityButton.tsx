import type {MouseEvent} from "react";
import {Button} from "@/shared/ui/button";

import styles from "./QuantityButton.module.css"
import MinusIcon from "@/shared/assets/icons/minus.svg?react";
import PlusIcon from "@/shared/assets/icons/plus.svg?react";
import type {ComponentSize} from "@/shared/lib";


interface QuantityButtonProps {
    size?: ComponentSize;
    quantity: number;
    incQuantity: (event: MouseEvent, action: 'increment' | 'decrement') => void;
    decQuantity: (event: MouseEvent, action: 'increment' | 'decrement') => void;
}

export const QuantityButton = (
    {
        size = 'small',
        quantity,
        incQuantity,
        decQuantity,
    }: QuantityButtonProps
) => {

    return (
        <div className={styles.quantityButton}>
            <Button
                variant="secondary"
                size={size}
                icon={<MinusIcon className={styles.icon}/>}
                onClick={(event) => incQuantity(event, 'decrement')}
            />

            <span className={styles.quantity}>
                {quantity}
            </span>

            <Button
                variant="secondary"
                size={size}
                icon={<PlusIcon className={styles.icon}/>}
                onClick={(event: MouseEvent) => decQuantity(event, 'increment')}
            />
        </div>
    );
}