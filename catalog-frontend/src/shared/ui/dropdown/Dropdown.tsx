import styles from "./Dropdown.module.css";
import type {Placement} from "@floating-ui/dom";
import {useDisclosure} from "@/shared/lib";
import {useRef, useState} from "react";
import {Popover} from "@/shared/ui/popover";
import {Icon} from "@/shared/ui/icon";
import ArrowUpIcon from "@/shared/assets/icons/arrow-up.svg?react";
import ArrowDownIcon from "@/shared/assets/icons/arrow-down.svg?react";
import {DropdownRow} from "@/shared/ui/dropdown/DropdownRow.tsx";


export interface DropdownOption {
    id: number;
    label: string;
}

interface DropdownProps {
    options: DropdownOption[];
    onChange: (id: number) => void;
    startWith?: number;
    placement?: Placement;
}

interface DropdownProps {
    options: DropdownOption[];
    onChange: (id: number) => void;
    startWith?: number;
    placement?: Placement;
}

export const Dropdown = (
    {
        options,
        onChange,
        startWith = 0,
        placement,
    }: DropdownProps
) => {
    const {isOpen, toggle, close} = useDisclosure();
    const anchorRef = useRef<HTMLDivElement | null>(null);

    const [currentOption, setCurrentOption] = useState<DropdownOption | undefined>(() => {
        const index = Math.min(Math.max(startWith, 0), options.length - 1);
        return options[index];
    });

    const handleSelect = (id: number) => {
        setCurrentOption(options.find(option => option.id === id));
        onChange(id);
        close();
    }

    return (
        <>
            <div className={styles.trigger} data-open={isOpen} ref={anchorRef} onClick={toggle}>
                <span className={styles.label}>{currentOption?.label}</span>
                <Icon className={styles.icon}>
                    {isOpen ? <ArrowUpIcon/> : <ArrowDownIcon/>}
                </Icon>
            </div>

            <Popover
                isOpen={isOpen}
                onClose={close}
                anchorRef={anchorRef}
                placement={placement}
                matchWidth
            >
                {options.map(option => (
                    <DropdownRow
                        key={option.id}
                        label={option.label}
                        selected={option.id === currentOption?.id}
                        onSelect={() => handleSelect(option.id)}
                    />
                ))}
            </Popover>
        </>
    );
}