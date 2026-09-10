import type {CloseButtonProps} from "./CloseButton.types.ts";
import {Button} from "@/shared/ui/button";
import styles from "./CloseButton.module.css";
import CrossIcon from "@/shared/assets/icons/cross.svg?react";


export const CloseButton = (
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
        <Button
            {...props}
            variant={"neutral"}
            className={closeButtonStyles}
            icon={<CrossIcon/>}
            onClick={onClick}
        />
    );
}