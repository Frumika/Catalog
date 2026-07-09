import { useState} from "react";
import {sessionApi} from "../api/sessionApi.ts";
import {ApiError, tokenLocalStorage, toApiError} from "@/shared/api";
import {useClearSession, useSetTokens} from "./sessionStore.ts";


export const useSession = () => {
    const setTokens = useSetTokens();
    const clearSession = useClearSession();

    const [isCodeSend, setCodeSend] = useState(false);
    const [isLoading, setLoading] = useState(false);
    const [error, setError] = useState<ApiError | null>(null);

    const sendCode = async (email: string): Promise<void> => {
        setError(null);
        setCodeSend(true);
        setLoading(true);
        try {
            await sessionApi.sendCode(email);
        } catch (err) {
            setError(toApiError(err));
            setCodeSend(false);
        } finally {
            setLoading(false);
        }
    };

    const verify = async (email: string, code: string): Promise<void> => {
        setError(null);
        setLoading(true);
        try {
            const tokens = await sessionApi.verify(email, code);
            setTokens(tokens.accessToken, tokens.refreshToken);
            setCodeSend(false);
        } catch (err) {
            setError(toApiError(err));
        } finally {
            setLoading(false);
        }
    };

    const logout = async (): Promise<void> => {
        const refreshToken = tokenLocalStorage.getRefreshToken();
        if (!refreshToken) {
            clearSession();
            return;
        }

        setLoading(true);
        try {
            await sessionApi.logout(refreshToken);
        } catch (err) {
            setError(toApiError(err));
        } finally {
            clearSession();
            setCodeSend(false);
            setLoading(false);
        }
    };

    const logoutAll = async (): Promise<void> => {
        setLoading(true);
        try {
            await sessionApi.logoutAll();
        } catch (err) {
            setError(toApiError(err));
        } finally {
            clearSession();
            setCodeSend(false);
            setLoading(false);
        }
    };

    return {
        isCodeSend,
        isLoading,
        error,
        sendCode,
        verify,
        logout,
        logoutAll,
    };
};