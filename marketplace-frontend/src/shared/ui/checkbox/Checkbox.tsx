import styles from "./Chekcbox.module.css";
import CheckIcon from "@/shared/assets/icons/chekc.svg?react";


interface CheckboxProps {
    selected?: boolean;
    onChange?: () => void;
    className?: string;
}

export const Checkbox = (
    {
        selected = false,
        onChange,
        className,
    }: CheckboxProps
) => {

    const checkboxStyles = [
        styles.checkbox,
        selected ? styles.checked : null,
        className,
    ].filter(Boolean).join(' ');

    return (
        <div className={checkboxStyles}
             onClick={
                 (event) => {
                     event.stopPropagation();
                     onChange?.()
                 }
             }
        >
            {selected && <CheckIcon className={styles.icon}/>}
        </div>
    )
}