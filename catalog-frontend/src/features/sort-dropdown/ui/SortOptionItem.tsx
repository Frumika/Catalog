import styles from "./SortOptionItem.module.css";
import type {SortOption} from "@/features/sort-dropdown/model/types.ts";
import {Icon} from "@/shared/ui/icon";
import ChekcIcon from "@/shared/assets/icons/chekc.svg?react";


interface SortOptionItemProps {
    option: SortOption;
    selected: boolean;
    onSelect: (id: number) => void;
}

export const SortOptionItem = (
    {
        option,
        selected,
        onSelect,
    }: SortOptionItemProps
) => {
    const sortOptionStyles = [
        styles.sortOptionItem,
        selected ? styles.selected : null
    ].filter(Boolean).join(' ');

    return (
        <div className={sortOptionStyles}
             onClick={() => onSelect(option.id)}
        >
            <span className={styles.label}>
                {option.label}
            </span>

            {selected && (
                <Icon
                    className={styles.icon}
                    size={"medium"}>
                    {<ChekcIcon/>}
                </Icon>
            )}
        </div>
    )
}