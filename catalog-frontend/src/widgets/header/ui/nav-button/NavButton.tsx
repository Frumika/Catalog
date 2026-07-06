import type {NavButtonProps} from "@/widgets/header/ui/nav-button/NavButton.types.ts";
import styles from "./NavButton.module.css";
import {Badge} from "@/shared/ui/badge";
import {Icon} from "@/shared/ui/icon";


export const NavButton = (
    {
        icon,
        badgeValue,
        badgeVisible = true,
        children,
        className,
        displayMode = "full",
        ...props
    }: NavButtonProps) => {

    const isCompact = displayMode === "compact";
    const badgeVariant = badgeValue !== undefined ? "value" : "dot";

    const navButtonStyles = [
        styles.navButton,
        isCompact ? styles.hideText : null,
        className,
    ].filter(Boolean).join(' ');

    const badgeStyles = [
        badgeVariant === "value" ? styles.badgeValue : styles.badgeDot,
    ].filter(Boolean).join(' ');

    return (
        <button {...props} className={navButtonStyles}>
            <span className={styles.content}>
                {icon && <Icon size="medium">{icon}</Icon>}
                {!isCompact && <span>{children}</span>}
            </span>

            <Badge
                className={badgeStyles}
                value={badgeValue}
                visible={badgeVisible}
                variant={badgeVariant}
            />
        </button>
    )
};