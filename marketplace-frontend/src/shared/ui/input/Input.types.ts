import type {InputHTMLAttributes} from "react"


export interface InputProps extends Omit<InputHTMLAttributes<HTMLInputElement>, 'onChange'> {
    value?: string,
    onChange?: (value: string) => void,
}