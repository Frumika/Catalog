import type {AddressButtonProps} from "./AddressButton.types.ts";
import styles from "./AddressButton.module.css";


export const AddressButton = (
    {
        address,
        className,
        ...props
    }: AddressButtonProps
) => {

    const hasAddress = !!address;
    const label: string = hasAddress ? "Пункт выдачи •" : "Укажите пункт выдачи •";
    const destination: string = hasAddress ? address : "Выбрать";

    const deliveryButtonStyles = [
        styles.addressButton,
        !hasAddress && styles.emptyAddress,
        className,
    ].filter(Boolean).join(' ');

    return (
        <button
            {...props}
            className={deliveryButtonStyles}>
            <span className={styles.label}>{label}</span>
            <span className={styles.destination}>{destination}</span>
        </button>
    );
}