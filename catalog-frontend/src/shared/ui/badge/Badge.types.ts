import type {HTMLProps} from "react";

export interface BadgeProps extends HTMLProps<HTMLDivElement> {
    value?: number;
    max?: number;
    className?: string;
}