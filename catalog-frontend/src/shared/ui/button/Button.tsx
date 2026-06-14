import type {ButtonProps} from "./Button.types.ts";
import styles from './Button.module.css';

export const Button = (
    {
        children,
        icon,
        variant = 'primary',
        size = 'medium',
        fullWidth = false,
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
            className={buttonClasses} {...props}>
            {icon && <span className={styles.iconWrapper}>{icon}</span>}
            {children && <span className={styles.content}>{children}</span>}
        </button>
    );
};