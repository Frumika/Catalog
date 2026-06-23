import type {ButtonHTMLAttributes, ReactNode} from "react";


export interface NavButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    icon?: ReactNode;
    badgeValue?: number;
    badgeVisible?: boolean;
    hideText?: boolean;
}