import type {LogoProps} from "@/shared/ui/logo/Logo.types.ts";
import styles from "./Logo.module.css";
import LogoPic from "@/shared/assets/logo-pic.svg?react";
import LogoText from "@/shared/assets/logo-text.svg?react";


export const Logo = (
    {
        disabled = false,
        className,
        onClick,
        hideText = false,
        ...props
    }: LogoProps
) => {

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
            onClick={onClick}
        >
            <LogoPic className={styles.icon}/>
            {!hideText && <LogoText className={styles.text} />}
        </button>
    );
}