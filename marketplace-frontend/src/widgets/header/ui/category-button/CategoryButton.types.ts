import type {ButtonHTMLAttributes, ReactNode} from "react";

export interface CategoryButtonProps extends ButtonHTMLAttributes<HTMLButtonElement>{
    icon?: ReactNode;
}