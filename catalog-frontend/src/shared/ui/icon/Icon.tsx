import type {IconProps} from "@/shared/ui/icon/Icon.types.ts";
import styles from './Icon.module.css';


export const Icon = (
    {
        children,
        size = "medium",
        className,
    }: IconProps) => {

    const iconClasses = [
        styles.icon,
        styles[size],
        className,
    ].filter(Boolean).join(' ');

    return (
        <span
            className={iconClasses}>
            {children}
        </span>
    );
};