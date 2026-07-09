import type {LogoProps} from "@/shared/ui/logo/Logo.types.ts";
import {useNavigate} from "react-router-dom";
import LogoPic from "@/shared/assets/icons/logo-pic.svg?react";
import LogoText from "@/shared/assets/icons/logo-text.svg?react";
import styles from "./Logo.module.css";


export const Logo = (
    {
        disabled = false,
        className,
        displayMode = "full",
        ...props
    }: LogoProps
) => {

    const navigate = useNavigate();
    const isCompact = displayMode === "compact";

    const logoClasses = [
        styles.logo,
        !disabled ? styles.enabled : null,
        className,
    ].filter(Boolean).join(' ');

    return (
        <button
            {...props}
            type="button"
            className={logoClasses}
            disabled={disabled}
            onClick={() => navigate('/')}
        >
            <LogoPic className={styles.icon}/>
            {!isCompact && <LogoText className={styles.text}/>}
        </button>
    );
}