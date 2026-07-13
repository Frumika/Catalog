import styles from './Button.module.css';
import {Icon} from "@/shared/ui/icon";
import type {ButtonHTMLAttributes, ReactNode} from "react";
import type {ComponentSize} from "@/shared/lib";


export type ButtonVariant = 'primary' | 'secondaryGhost' | 'secondary' | 'ghost' | 'neutral';

export interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    icon?: ReactNode;
    variant?: ButtonVariant;
    size?: ComponentSize;
    fullWidth?: boolean;
}

export const Button = (
    {
        icon,
        variant = 'primary',
        size = 'medium',
        fullWidth = false,
        children,
        className,
        ...props
    }: ButtonProps) => {

    const isIconOnly = !children && !!icon

    const buttonClasses = [
        styles.button,
        styles[variant],
        styles[size],
        fullWidth ? styles.fullWidth : null,
        isIconOnly ? styles.iconOnly : null,
        className,
    ].filter(Boolean).join(' ');

    return (
        <button
            {...props}
            className={buttonClasses}>
            {icon && <Icon size={size}>{icon}</Icon>}
            {children}
        </button>
    );
};