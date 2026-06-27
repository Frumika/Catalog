import type {AddressItemProps} from "./AddressItem.types.ts";
import KebabMenuIcon from "@/shared/assets/icons/kebab-menu.svg?react";
import styles from "./AddressItem.module.css";
import {Button} from "@/shared/ui/button";


export const AddressItem = (
    {
        deliveryAddress,
        selected = false,
        onSelect,
        className
    }: AddressItemProps
) => {

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
                className={styles.kebabMenuButton}
                variant={"ghost"}
                size={"medium"}
                icon={<KebabMenuIcon/>}
                onClick={(event) => {
                    event.stopPropagation();
                }}
            />
        </div>
    );
}