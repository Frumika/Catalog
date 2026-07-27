import styles from "./SortDropdown.module.css";
import {SORT_OPTIONS} from "@/features/sort-dropdown/model/types.ts";
import {Dropdown} from "@/shared/ui/dropdown";


interface SortDropdownProps {
    onSelect: (id: number) => void;
}

export const SortDropdown = ({onSelect}: SortDropdownProps) => (

    <div className={styles.sortDropdown}>
        <Dropdown
            options={SORT_OPTIONS}
            onChange={onSelect}
            placement={"bottom-start"}
        />
    </div>
)
