import styles from "./Notification.module.css";
import {CloseButton} from "@/shared/ui/close-button";
import type {NotificationType} from "@/shared/lib";


interface NotificationProps {
    type: NotificationType;
    message: string;
    onClose?: () => void;
}

export const Notification = (
    {
        type,
        message,
        onClose,
    }: NotificationProps
) => {

    let displayedType: string;
    switch (type) {
        case "info": {
            displayedType = "Справка";
            break;
        }
        case "success": {
            displayedType = "Успех";
            break;
        }
        case "warning": {
            displayedType = "Внимание";
            break;
        }
        default: {
            displayedType = "Ошибка";
        }
    }

    return (
        <div className={styles.notification}>
            <div className={styles.header}>
                <p className={styles.type}>{displayedType}</p>
            </div>

            <div className={styles.content}>
                <div className={styles.message}>{message}</div>
            </div>

            <CloseButton className={styles.closeButton} onClick={onClose}/>
        </div>
    )
}