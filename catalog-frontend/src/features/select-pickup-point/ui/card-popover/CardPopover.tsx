import styles from "./CardPopover.module.css";
import {Popover} from "@/shared/ui/popover";
import {Button} from "@/shared/ui/button";
import type {RefObject} from "react";
import TrashIcon from "@/shared/assets/icons/trashcan.svg?react";
import CopyIcon from "@/shared/assets/icons/copy.svg?react";


interface CardPopoverProps {
    isOpen: boolean;
    onClose: () => void;
    anchorRef: RefObject<HTMLElement | null>;
}

export const CardPopover = (
    {
        isOpen,
        onClose,
        anchorRef,
    }: CardPopoverProps
) => {

    const removeButtonStyles = [
        styles.button,
        styles.removeButton
    ].filter(Boolean).join(' ');


    return (
        <Popover isOpen={isOpen}
                 onClose={onClose}
                 anchorRef={anchorRef}>

            <div className={styles.content}>
                <Button
                    className={styles.button}
                    fullWidth
                    variant={"popover"}
                    icon={<CopyIcon/>}
                    size={"small"}>
                    Копировать адрес
                </Button>

                <Button
                    className={removeButtonStyles}
                    fullWidth
                    variant={"popover"}
                    icon={<TrashIcon/>}
                    size={"small"}
                >
                    Удалить
                </Button>
            </div>
        </Popover>
    );
}