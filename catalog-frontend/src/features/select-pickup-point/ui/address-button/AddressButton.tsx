import styles from "./AddressButton.module.css";
import {useCurrentPickupPoint} from "@/entities/pickup-point/model/pickupPointStore.ts";


interface AddressButtonProps {
    onClick: () => void;
    className?: string;
}

export const AddressButton = (
    {
        onClick,
        className,
        ...props
    }: AddressButtonProps
) => {
    const currentPickupPoint = useCurrentPickupPoint();

    const hasAddress = !!currentPickupPoint;
    const label: string = hasAddress ? "Пункт выдачи •" : "Укажите пункт выдачи •";
    const destination: string = hasAddress ? currentPickupPoint.address : "Выбрать";

    const deliveryButtonStyles = [
        styles.addressButton,
        !hasAddress && styles.emptyAddress,
        className,
    ].filter(Boolean).join(' ');

    return (
        <button
            {...props}
            onClick={onClick}
            className={deliveryButtonStyles}>
            <span className={styles.label}>{label}</span>
            <span className={styles.destination}>{destination}</span>
        </button>
    );
}