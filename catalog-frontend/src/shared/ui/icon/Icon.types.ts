import type {ReactNode} from "react";
import type {ButtonSize} from "@/shared/ui/button";

export type IconSize = "small" | "medium" | "large";

export interface IconProps {
    children: ReactNode;
    size?: ButtonSize;
    interactive?: boolean;
    className?: string;
}