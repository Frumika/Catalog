import type {ButtonHTMLAttributes, ReactNode} from "react";

export type ButtonVariant = 'primary' | 'secondary' | 'ghost' | 'neutral';

export type ButtonSize = 'small' | 'medium' | 'large';

export interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    icon?: ReactNode;
    variant?: ButtonVariant;
    size?: ButtonSize;
    fullWidth?: boolean;
}