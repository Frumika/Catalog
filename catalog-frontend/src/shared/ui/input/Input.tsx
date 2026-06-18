import type {InputProps} from "@/shared/ui/input/Input.types.ts";
import styles from "./Input.module.css";
import type {ChangeEvent} from "react";

export const Input = (
    {
        value,
        onChange,
        className,
        ...props
    }: InputProps) => {

    const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
        onChange?.(event.target.value)
    }

    const inputClasses = [
        styles.input,
        className,
    ].filter(Boolean).join(' ');

    return (
        <input
            className={inputClasses}
            value={value}
            onChange={handleChange}
            {...props}
        />
    );
}