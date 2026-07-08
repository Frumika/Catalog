import {useEffect, useState} from "react";
import type {Session} from "./types.ts";
import {sessionApi} from "../api/sessionApi.ts";
import {ApiError, localSessionStorage, toApiError} from "@/shared/api";


export const useSession = () => {
    const [session, setSession] = useState<Session | null>(null);
    const [isCodeSend, setCodeSend] = useState(false);
    const [isLoading, setLoading] = useState(true);
    const [error, setError] = useState<ApiError | null>(null);

    const isVerify = session !== null;

    useEffect(() => {
        const sessionId: string | null = localSessionStorage.get();
        if (!sessionId) {
            setLoading(false);
            return;
        }

        sessionApi.getSession(sessionId)
            .then(user => setSession(user))
            .catch(error => {
                const apiError = toApiError(error);
                if (apiError.code === "user_session_not_found") {
                    localSessionStorage.clear();
                }
                setError(apiError);
            })
            .finally(() => setLoading(false));
    }, []);

    const sendCode = async (email: string): Promise<void> => {
        try {
            await sessionApi.sendCode(email);
            setError(null);
            setCodeSend(true);
        } catch (error) {
            setError(toApiError(error));
            setCodeSend(false);
        }
    };

    const verify = async (email: string, code: string): Promise<void> => {
        try {
            const user = await sessionApi.verify(email, code);
            localSessionStorage.set(user.sessionId);
            setError(null);
            setCodeSend(false);
            setSession(user);
        } catch (error) {
            setError(toApiError(error));
        }
    };

    const logout = async (): Promise<void> => {
        const sessionId = localSessionStorage.get();
        if (!sessionId) {
            return;
        }
        try {
            await sessionApi.logout(sessionId);
        } catch (error) {
            setError(toApiError(error));
        } finally {
            localSessionStorage.clear();
            setSession(null);
            setCodeSend(false);
        }
    };

    const logoutAll = async (): Promise<void> => {
        const sessionId = localSessionStorage.get();
        if (!sessionId) {
            return;
        }
        try {
            await sessionApi.logoutAll(sessionId);
        } catch (error) {
            setError(toApiError(error));
        } finally {
            localSessionStorage.clear();
            setSession(null);
            setCodeSend(false);
        }
    };

    return {
        session,
        isVerify,
        isCodeSend,
        isLoading,
        error,
        sendCode,
        verify,
        logout,
        logoutAll,
    };
};