import type {BadgeProps} from "@/shared/ui/badge/Badge.types.ts";
import styles from "./Badge.module.css"


export const Badge = (
    {
        variant = "value",
        value,
        max = 99,
        visible = true,
        className,
    }: BadgeProps) => {

    if (!visible) return null;

    if (variant === 'value' && (value === undefined || value <= 0)) {
        return null;
    }

    const isValueMode = variant === 'value';
    const hasOverflow = isValueMode && value !== undefined && value > max;
    const displayValue = hasOverflow ? `${max}+` : value;

    const badgeClasses = [
        styles.badge,
        isValueMode ? styles.value : styles.dot,
        className,
    ].filter(Boolean).join(' ');

    return (
        <span className={badgeClasses} key={value}>
            {isValueMode ? displayValue : null}
        </span>
    );
};