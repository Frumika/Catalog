import type {DeliveryButtonProps} from "./DeliveryButton.types.ts";
import styles from "./DeliveryButton.module.css";


export const DeliveryButton = (
    {
        address,
        className,
        ...props
    }: DeliveryButtonProps
) => {

    const hasAddress = !!address;
    const label: string = hasAddress ? "Пункт выдачи •" : "Укажите пункт выдачи •";
    const destination: string = hasAddress ? address : "Выбрать";

    const deliveryButtonStyles = [
        styles.deliveryButton,
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