import styles from "./SortDropdown.module.css";
import {Popover} from "@/shared/ui/popover";
import {useDisclosure} from "@/shared/lib";
import {useRef} from "react";
import {SORT_OPTIONS} from "@/features/sort-dropdown/model/types.ts";
import {SortOptionItem} from "@/features/sort-dropdown/ui/SortOptionItem.tsx";
import {Icon} from "@/shared/ui/icon";
import ArrowUpIcon from "@/shared/assets/icons/arrow-up.svg?react";
import ArrowDownIcon from "@/shared/assets/icons/arrow-down.svg?react";


interface SortDropdownProps {
    currentSortOptionId?: number;
    onSelect: (id: number) => void;
}

export const SortDropdown = (
    {
        currentSortOptionId,
        onSelect,
    }: SortDropdownProps
) => {

    const {isOpen, toggle, close} = useDisclosure();
    const anchorRef = useRef<HTMLDivElement | null>(null);
    const currentOption = SORT_OPTIONS.find(so => so.id === currentSortOptionId);

    return (
        <>
            <div className={styles.currentSortOption}
                 data-open={isOpen}
                 ref={anchorRef}
                 onClick={toggle}
            >

                <span className={styles.label}>
                    {currentOption?.label}
                </span>

                {isOpen ?
                    (<Icon className={styles.icon}><ArrowUpIcon/></Icon>)
                    :
                    (<Icon className={styles.icon}><ArrowDownIcon/></Icon>)
                }
            </div>

            <Popover
                className={styles.popover}
                isOpen={isOpen}
                onClose={close}
                anchorRef={anchorRef}
                placement={"bottom-start"}
            >
                {SORT_OPTIONS.map(option => (
                    <SortOptionItem
                        key={option.id}
                        option={option}
                        selected={option.id == currentSortOptionId}
                        onSelect={onSelect}
                    />
                ))}
            </Popover>
        </>
    )
}