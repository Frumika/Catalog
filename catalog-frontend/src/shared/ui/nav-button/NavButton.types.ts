import type {ButtonHTMLAttributes, ReactNode} from "react";
import type {ComponentDisplayMode} from "@/shared/lib";


export interface NavButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    icon?: ReactNode;
    badgeValue?: number;
    badgeVisible?: boolean;
    displayMode?: ComponentDisplayMode;
}