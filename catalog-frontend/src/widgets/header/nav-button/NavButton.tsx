import type {NavButtonProps} from "@/widgets/header/nav-button/NavButton.types.ts";
import styles from "./NavButton.module.css";
import {Badge} from "@/shared/ui/badge";


export const NavButton = (
    {
        icon,
        badgeValue,
        children,
        className,
        ...props
    }: NavButtonProps) => {

    const navButtonStyles = [
        styles.navButton,
        className,
    ].filter(Boolean).join(' ');

    return (
        <button
            {...props}
            className={navButtonStyles}>
            <span className={styles.iconWrapper}>
                {icon}
                <Badge value={badgeValue} />
            </span>

            {children}
        </button>
    )
};