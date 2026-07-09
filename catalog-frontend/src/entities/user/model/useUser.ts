import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import type {User} from "@/entities/user";
import {userApi} from "@/entities/user/api/userApi.ts";

export const useUser = (isAuthenticated: boolean) => {
    const [user, setUser] = useState<User | null>(null);
    const [isLoading, setLoading] = useState(false);
    const [error, setError] = useState<ApiError | null>(null);

    useEffect(() => {
        if (!isAuthenticated) {
            setUser(null)
            return;
        }
        setLoading(true);
        userApi.getUser()
            .then(user => setUser(user))
            .catch(error => setError(toApiError(error)))
            .finally(() => setLoading(false));
    }, [isAuthenticated]);

    return {
        user,
        isLoading,
        error,
    }
}