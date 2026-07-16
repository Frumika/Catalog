import styles from './Icon.module.css';
import type {ReactNode} from "react";
import type {ComponentSize} from "@/shared/lib";


interface IconProps {
    children: ReactNode;
    size?: ComponentSize;
    className?: string;
}

export const Icon = (
    {
        children,
        size = "medium",
        className,
    }: IconProps) => {

    const iconClasses = [
        styles.icon,
        styles[size],
        className,
    ].filter(Boolean).join(' ');

    return (
        <span
            className={iconClasses}>
            {children}
        </span>
    );
};