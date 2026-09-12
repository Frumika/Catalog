import type {ButtonHTMLAttributes} from "react";
import type {ComponentDisplayMode} from "@/shared/model";


export interface CatalogButtonProps extends ButtonHTMLAttributes<HTMLButtonElement>{
    displayMode?: ComponentDisplayMode;
}