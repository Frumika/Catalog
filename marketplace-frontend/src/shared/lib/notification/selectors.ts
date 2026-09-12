import {useNotificationStore} from "@/shared/lib/notification/useNotificationStore.ts";


export const useGetNotifications = () =>
    useNotificationStore(s => s.notifications);

export const useNotify = () =>
    useNotificationStore(s => s.notify);

export const useRemoveNotification = () =>
    useNotificationStore(s => s.removeNotification);