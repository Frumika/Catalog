import type {ButtonHTMLAttributes} from "react";

export interface CatalogButtonProps extends ButtonHTMLAttributes<HTMLButtonElement>{
    hideText?: boolean;
}