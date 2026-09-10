import styles from "./NotificationContainer.module.css";
import {Notification} from "@/shared/ui/notification"
import {type NotificationType, useGetNotifications, useRemoveNotification} from "@/shared/lib";
import {useEffect, useState} from "react";
import {createPortal} from "react-dom";


interface WrapperProps {
    id: string;
    type: NotificationType;
    message: string;
    onClose: () => void;
    isHovered: boolean;
}

const NotificationTimerWrapper = (
    {
        type,
        message,
        onClose,
        isHovered
    }: WrapperProps) => {

    useEffect(() => {
        if (isHovered) return;

        const timer = setTimeout(() => {
            onClose();
        }, 1500);

        return () => clearTimeout(timer);
    }, [isHovered, onClose]);

    return (
        <div className={styles.timerWrapper}>
            <Notification type={type} message={message} onClose={onClose}/>
        </div>
    );
};


const MAX_NOTIFICATIONS = 3;

export const NotificationContainer = () => {
    const notifications = useGetNotifications();
    const removeNotification = useRemoveNotification();
    const [isGroupHovered, setIsGroupHovered] = useState(false);

    if (notifications.length === 0) {
        return null;
    }

    const visibleNotifications = [...notifications].reverse().slice(0, MAX_NOTIFICATIONS);

    return createPortal(
        <div className={styles.notificationContainer}
             onMouseEnter={() => setIsGroupHovered(true)}
             onMouseLeave={() => setIsGroupHovered(false)}
        >
            {visibleNotifications.map((notification) => (
                <NotificationTimerWrapper
                    key={notification.id}
                    id={notification.id}
                    type={notification.type}
                    message={notification.message}
                    isHovered={isGroupHovered}
                    onClose={() => removeNotification(notification.id)}
                />
            ))}
        </div>, document.body
    );
};