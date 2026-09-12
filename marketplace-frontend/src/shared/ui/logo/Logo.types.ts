import type {ButtonHTMLAttributes} from "react";
import type {ComponentDisplayMode} from "@/shared/model";

export interface LogoProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    displayMode?: ComponentDisplayMode;
}