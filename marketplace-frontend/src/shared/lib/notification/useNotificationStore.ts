import {create} from "zustand";
import type {NotificationType} from "@/shared/model/contracts.ts";


interface NotificationItem {
    id: string;
    type: NotificationType;
    message: string;
}

interface NotificationState {
    notifications: NotificationItem[];
    notify: (type: NotificationType, message: string) => void;
    removeNotification: (id: string) => void;
}

export const useNotificationStore = create<NotificationState>(
    (set) => {
        return {
            notifications: [],

            notify: (type, message) => {
                const id = crypto.randomUUID();
                set((state) => ({
                    notifications: [...state.notifications, {id, type, message}]
                }));
            },

            removeNotification: (id) =>
                set((state) => ({
                    notifications: state.notifications.filter((n) => n.id !== id)
                }))
        }
    }
);