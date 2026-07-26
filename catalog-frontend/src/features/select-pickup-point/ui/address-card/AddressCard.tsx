import type {PickupPoint} from "@/entities/pickup-point";
import KebabMenuIcon from "@/shared/assets/icons/kebab-menu.svg?react";
import styles from "./AddressCard.module.css";
import {Button} from "@/shared/ui/button";
import {CardPopover} from "../card-popover/CardPopover.tsx";
import {useRef} from "react";
import {useDisclosure} from "@/shared/lib";


interface AddressCardProps {
    pickupPoint: PickupPoint,
    selected?: boolean,
    onSelect?: (pickupPoint: PickupPoint) => void
    className?: string,
}

export const AddressCard = (
    {
        pickupPoint,
        selected = false,
        onSelect,
        className
    }: AddressCardProps
) => {

    const anchorRef = useRef<HTMLButtonElement>(null);
    const {isOpen, open, close} = useDisclosure();

    const addressItemStyles = [
        styles.addressItem,
        selected ? styles.selected : styles.hasHover,
        className,
    ].filter(Boolean).join(' ');

    return (
        <div
            className={addressItemStyles}
            onClick={() => onSelect?.(pickupPoint)}
        >
            <header className={styles.header}>
                <span className={styles.title}>Пункт выдачи</span>
                <span className={styles.id}>{`№ ${pickupPoint.id}`}</span>
            </header>

            <div className={styles.description}>
                <span className={styles.address}>
                    {pickupPoint.address}
                </span>

                <span className={styles.shelfLife}>
                    {`Срок хранения заказа – ${pickupPoint.shelfLifetime} дней`}
                </span>
            </div>

            <Button
                ref={anchorRef}
                className={styles.kebabMenuButton}
                variant={"ghost"}
                size={"medium"}
                icon={<KebabMenuIcon/>}
                onClick={(event) => {
                    event.stopPropagation();
                    open();
                }}
            />

            <CardPopover
                pickupPoint={pickupPoint}
                isOpen={isOpen}
                onClose={close}
                anchorRef={anchorRef}
            />
        </div>
    );
}