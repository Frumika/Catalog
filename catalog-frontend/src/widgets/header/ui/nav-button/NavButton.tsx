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
                {icon && <Icon size={"medium"}>{icon}</Icon>}
                <Badge value={badgeValue}/>
            </span>

            {children}
        </button>
    )
};