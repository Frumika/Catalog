import type {NavButtonProps} from "@/shared/ui/nav-button/NavButton.types.ts";
import style from "./NavButton.module.css";


export const NavButton = (
    {
        icon,
        children,
        className,
        ...props
    }: NavButtonProps) => {

    const navButtonStyles = [
        style.navButton,
        className,
    ].filter(Boolean).join(' ');

    return (
        <button
            {...props}
            className={navButtonStyles}
        >
            {icon}
            {children}
        </button>
    )
};