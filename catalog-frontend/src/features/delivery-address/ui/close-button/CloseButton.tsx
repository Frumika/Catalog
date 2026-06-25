import type {CloseButtonProps} from "./CloseButton.types.ts";
import {Icon} from "@/shared/ui/icon";
import CrossIcon from "@/shared/assets/cross.svg?react";

import styles from "./CloseButton.module.css";


export const    CloseButton = (
    {
        onClick,
        className,
        ...props
    }: CloseButtonProps
) => {

    const closeButtonStyles = [
        styles.closeButton,
        className,
    ].filter(Boolean).join(' ');

    return (
        <button
            {...props}
            className={closeButtonStyles}
            onClick={onClick}
        >
            <Icon size={"medium"}><CrossIcon/></Icon>
        </button>
    );
}