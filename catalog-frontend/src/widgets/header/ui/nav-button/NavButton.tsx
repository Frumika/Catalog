import type {NavButtonProps} from "@/widgets/header/ui/nav-button/NavButton.types.ts";
import styles from "./NavButton.module.css";
import {Badge} from "@/shared/ui/badge";
import {Icon} from "@/shared/ui/icon";


export const NavButton = (
    {
        icon,
        badgeValue,
        children,
        className,
        hideText = false,
        ...props
    }: NavButtonProps) => {

    const navButtonStyles = [
        styles.navButton,
        hideText ? styles.hideText : null,
        className,
    ].filter(Boolean).join(' ');

    return (
        <button {...props} className={navButtonStyles}>
            <span className={styles.content}>
                {icon && <Icon size="medium">{icon}</Icon>}
                {!hideText && <span>{children}</span>}
            </span>

            <Badge value={badgeValue}/>
        </button>
    )
};