import type {ButtonHTMLAttributes} from "react";
import {Button} from "@/shared/ui/button";
import styles from "./LinkButton.module.css";


interface LinkButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {

}

export const LinkButton = (
    {
        children,
        className,
        onClick,
        ...props
    }: LinkButtonProps) => {

    const linkButtonStyles = [
        styles.linkButton,
        className,
    ].filter(Boolean).join(' ');

    return (
        <Button
            {...props}
            className={linkButtonStyles}
            variant={"ghost"}
            size={"small"}
            fullWidth={true}
            onClick={onClick}
        >
            {children}
        </Button>
    );
};