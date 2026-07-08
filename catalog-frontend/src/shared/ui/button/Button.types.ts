import type {ButtonHTMLAttributes, ReactNode} from "react";
import type {ComponentSize} from "@/shared/lib";

export type ButtonVariant = 'primary' | 'secondaryGhost' | 'secondary' | 'ghost' | 'neutral';


export interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    icon?: ReactNode;
    variant?: ButtonVariant;
    size?: ComponentSize;
    fullWidth?: boolean;
}