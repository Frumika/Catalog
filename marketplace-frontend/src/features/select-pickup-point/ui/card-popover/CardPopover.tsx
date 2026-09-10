import styles from "./CardPopover.module.css";
import {Popover} from "@/shared/ui/popover";
import {Button} from "@/shared/ui/button";
import type {RefObject} from "react";
import TrashIcon from "@/shared/assets/icons/trashcan.svg?react";
import CopyIcon from "@/shared/assets/icons/copy.svg?react";
import {type PickupPoint, usePickupPointActions} from "@/entities/pickup-point";


interface CardPopoverProps {
    pickupPoint: PickupPoint;
    isOpen: boolean;
    onClose: () => void;
    anchorRef: RefObject<HTMLButtonElement | null>;
}

export const CardPopover = (
    {
        pickupPoint,
        isOpen,
        onClose,
        anchorRef,
    }: CardPopoverProps
) => {

    const {removePickupPoint} = usePickupPointActions();

    return (
        <Popover isOpen={isOpen}
                 onClose={onClose}
                 anchorRef={anchorRef}>

            <Button
                fullWidth
                variant={"popover"}
                icon={<CopyIcon/>}
                size={"small"}
                onClick={async (event) => {
                    event.stopPropagation();

                    try {
                        await navigator.clipboard.writeText(pickupPoint.address);
                    } catch (error) {
                        console.error("Не удалось скопировать адрес: ", error);
                    }
                }}
            >
                Копировать адрес
            </Button>

            <Button
                className={styles.removeButton}
                fullWidth
                variant={"popover"}
                icon={<TrashIcon/>}
                size={"small"}
                onClick={async (event) => {
                    event.stopPropagation();
                    try {
                        await removePickupPoint(pickupPoint);
                    } catch (error) {
                        console.log("Что-то пошло не так, надо разобраться");
                    }
                }}
            >
                Удалить
            </Button>

        </Popover>
    );
}