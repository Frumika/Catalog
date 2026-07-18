import type {AddressCardProps} from "./AddressCard.types.ts";
import KebabMenuIcon from "@/shared/assets/icons/kebab-menu.svg?react";
import styles from "./AddressCard.module.css";
import {Button} from "@/shared/ui/button";
import {CardPopover} from "../card-popover/CardPopover.tsx";
import {useCardPopover} from "../../model/useCardPopover.ts";
import {useRef} from "react";


export const AddressCard = (
    {
        deliveryAddress,
        selected = false,
        onSelect,
        className
    }: AddressCardProps
) => {

    const anchorRef = useRef<HTMLButtonElement>(null);
    const {isOpen, open, close} = useCardPopover();

    const addressItemStyles = [
        styles.addressItem,
        selected ? styles.selected : styles.hasHover,
        className,
    ].filter(Boolean).join(' ');

    return (
        <div
            className={addressItemStyles}
            onClick={() => onSelect?.(deliveryAddress.id)}
        >
            <header className={styles.header}>
                <span className={styles.title}>Пункт выдачи</span>
                <span className={styles.id}>{`№ ${deliveryAddress.id}`}</span>
            </header>

            <div className={styles.description}>
                <span className={styles.address}>
                    {deliveryAddress.address}
                </span>

                <span className={styles.shelfLife}>
                    {`Срок хранения заказа – ${deliveryAddress.shelfLifetime} дней`}
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

            <CardPopover isOpen={isOpen}
                         onClose={close}
                         anchorRef={anchorRef}
            />
        </div>
    );
}