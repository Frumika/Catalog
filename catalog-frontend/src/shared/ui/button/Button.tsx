import type {ButtonProps} from "./Button.types.ts";
import styles from './Button.module.css';

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

    const isIconOnly = !children && !!icon;

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
            {icon}
            {children}
        </button>
    );
};