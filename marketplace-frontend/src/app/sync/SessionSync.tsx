import {useIsAuthenticated} from "@/entities/session";
import {useEffect, useRef} from "react";
import {useNotify} from "@/shared/lib/notification";


export const SessionSync = () => {
    const isAuthenticated = useIsAuthenticated();
    const wasAuthenticated = useRef(isAuthenticated);
    const notify = useNotify();

    useEffect(() => {
        if (wasAuthenticated.current && !isAuthenticated) {
            notify("warning", "Сессия истекла, войдите снова");
        }
        wasAuthenticated.current = isAuthenticated;
    }, [isAuthenticated]);

    return null;
};