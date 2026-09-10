import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import type {User} from "@/entities/user";
import {userApi} from "@/entities/user/api/userApi.ts";
import {useNotify} from "@/shared/lib";

export const useUser = (isAuthenticated: boolean) => {
    const [user, setUser] = useState<User | null>(null);
    const [isLoading, setLoading] = useState(false);
    const notify = useNotify();


    useEffect(() => {
        if (!isAuthenticated) {
            setUser(null)
            return;
        }
        setLoading(true);
        userApi.getUser()
            .then(user => setUser(user))
            .catch(error => {
                const apiError: ApiError = toApiError(error);
                notify("error", apiError.message || "Не удалось получить данные пользователя");
            })
            .finally(() => setLoading(false));
    }, [isAuthenticated]);


    return {
        user,
        isLoading
    }
}