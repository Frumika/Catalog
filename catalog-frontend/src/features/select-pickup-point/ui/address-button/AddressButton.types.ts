import type {ButtonHTMLAttributes} from "react";


export interface AddressButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    address?: string;
}