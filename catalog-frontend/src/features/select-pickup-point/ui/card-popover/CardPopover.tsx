import styles from "./CardPopover.module.css";
import {Popover} from "@/shared/ui/popover";
import {Button} from "@/shared/ui/button";
import type {RefObject} from "react";
import TrashIcon from "@/shared/assets/icons/trashcan.svg?react";
import CopyIcon from "@/shared/assets/icons/copy.svg?react";
import {type PickupPoint, useRemovePickupPoint} from "@/entities/pickup-point";
import {pickupPointApi} from "@/entities/pickup-point/api/pickupPointApi.ts";


interface CardPopoverProps {
    pickupPoint: PickupPoint;
    isOpen: boolean;
    onClose: () => void;
    anchorRef: RefObject<HTMLElement | null>;
}

export const CardPopover = (
    {
        pickupPoint,
        isOpen,
        onClose,
        anchorRef,
    }: CardPopoverProps
) => {

    const removePickupPoint = useRemovePickupPoint();

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
                    className={removeButtonStyles}
                    fullWidth
                    variant={"popover"}
                    icon={<TrashIcon/>}
                    size={"small"}
                    onClick={async (event) => {
                        event.stopPropagation();
                        try {
                            await pickupPointApi.remove(pickupPoint.id);
                            removePickupPoint(pickupPoint);
                        } catch (error) {
                            console.log("Что-то пошло не так, надо разобраться");
                        }
                    }}
                >
                    Удалить
                </Button>
            </div>
        </Popover>
    );
}