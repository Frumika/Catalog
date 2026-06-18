import type {LogoProps} from "@/shared/ui/logo/Logo.types.ts";
import styles from "./Logo.module.css";
import LogoIcon from "@/shared/assets/logo.svg?react"


export const Logo = (
    {
        disabled = false,
        className,
        onClick,
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
            <LogoIcon className={styles.image}/>
        </button>
    );
}