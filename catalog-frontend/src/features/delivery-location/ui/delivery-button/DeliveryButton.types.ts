import type {ButtonHTMLAttributes} from "react";


export interface DeliveryButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    address?: string;
}