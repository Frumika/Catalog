import type {BadgeProps} from "@/shared/ui/badge/Badge.types.ts";
import styles from "./Badge.module.css"


export const Badge = (
    {
        value,
        max = 99,
        className,
    }: BadgeProps) => {

    if (value === 0) return null;

    const hasValue = value !== undefined;
    const hasOverflow = hasValue && value > max;

    const badgeClasses = [
        styles.badge,
        hasValue ? styles.value : styles.dot,
        hasOverflow ? styles.overflow : null,
        className,
    ].filter(Boolean).join(' ');

    let displayValue: string | null;

    if (hasOverflow) {
        displayValue = `${max}+`;
    } else {
        displayValue = value ? value.toString() : null;
    }

    return (
        <span className={badgeClasses}
              key={value}>
            {displayValue}
        </span>
    );
};