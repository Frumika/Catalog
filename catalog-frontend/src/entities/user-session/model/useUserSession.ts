import {useEffect, useMemo, useState} from "react";
import {userSessionApi} from "../api/userSessionApi.ts";
import {ApiError, localSessionStorage, toApiError} from "@/shared/api";
import {
    useClearSession, useIsAuthenticated, useSessionEmail, useSessionId, useSessionLogin, useSetSession,
} from "@/entities/user-session/model/sessionStore.ts";
import type {UserSession} from "@/entities/user-session";


export const useUserSession = () => {
    const isAuthenticated = useIsAuthenticated();
    const sessionId = useSessionId();
    const email = useSessionEmail();
    const login = useSessionLogin();

    const setSession = useSetSession();
    const clearSession = useClearSession();

    const [isCodeSend, setCodeSend] = useState(false);
    const [isLoading, setLoading] = useState(true);
    const [error, setError] = useState<ApiError | null>(null);

    const session = useMemo<UserSession | null>(() => {
        if (!isAuthenticated || !sessionId || !email || !login) return null;
        return {sessionId, email, login};
    }, [isAuthenticated, sessionId, email, login]);

    useEffect(() => {
        const currentSessionId = localSessionStorage.get();
        if (!currentSessionId) {
            setLoading(false);
            return;
        }

        userSessionApi.getSession(currentSessionId)
            .then(user => setSession(user))
            .catch(error => {
                const apiError = toApiError(error);
                if (apiError.code === "user_session_not_found") {
                    clearSession();
                }
                setError(apiError);
            })
            .finally(() => setLoading(false));
    }, [setSession, clearSession]);

    const sendCode = async (email: string): Promise<void> => {
        try {
            await userSessionApi.sendCode(email);
            setError(null);
            setCodeSend(true);
        } catch (error) {
            setError(toApiError(error));
            setCodeSend(false);
        }
    };

    const verify = async (email: string, code: string): Promise<void> => {
        try {
            const newSession = await userSessionApi.verify(email, code);
            setSession(newSession);
            setError(null);
            setCodeSend(false);
        } catch (error) {
            setError(toApiError(error));
        }
    };

    const logout = async (): Promise<void> => {
        const currentSessionId = localSessionStorage.get();
        if (!currentSessionId) return;

        try {
            await userSessionApi.logout(currentSessionId);
        } catch (error) {
            setError(toApiError(error));
        } finally {
            clearSession();
            setCodeSend(false);
        }
    };

    const logoutAll = async (): Promise<void> => {
        const currentSessionId = localSessionStorage.get();
        if (!currentSessionId) return;

        try {
            await userSessionApi.logoutAll(currentSessionId);
        } catch (error) {
            setError(toApiError(error));
        } finally {
            clearSession();
            setCodeSend(false);
        }
    };

    return {
        session,
        isVerify: isAuthenticated,
        isCodeSend,
        isLoading,
        error,
        sendCode,
        verify,
        logout,
        logoutAll,
    };
};
