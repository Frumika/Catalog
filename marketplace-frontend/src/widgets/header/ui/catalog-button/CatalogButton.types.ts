import type {ButtonHTMLAttributes} from "react";
import type {ComponentDisplayMode} from "@/shared/lib";

export interface CatalogButtonProps extends ButtonHTMLAttributes<HTMLButtonElement>{
    displayMode?: ComponentDisplayMode;
}