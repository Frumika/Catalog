import styles from "./DropdownRow.module.css";
import {Icon} from "@/shared/ui/icon";
import CheckIcon from "@/shared/assets/icons/chekc.svg?react";


interface DropdownRowProps {
    label: string;
    selected: boolean;
    onSelect: () => void;
}

export const DropdownRow = (
    {
        label,
        selected,
        onSelect,
    }: DropdownRowProps
) => {
    return (
        <div className={styles.row} onClick={onSelect}>
            <span className={styles.label}>{label}</span>
            {selected && (
                <Icon className={styles.icon} size="medium">
                    <CheckIcon/>
                </Icon>
            )}
        </div>
    )
}