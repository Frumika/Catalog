import {useState} from "react";
import {sessionApi} from "../api/sessionApi.ts";
import {ApiError, tokenLocalStorage, toApiError} from "@/shared/api";
import {useClearSession, useSetTokens} from "./sessionStore.ts";
import {useNotify} from "@/shared/lib";


export const useSession = () => {
    const setTokens = useSetTokens();
    const clearSession = useClearSession();
    const notify = useNotify();

    const [isCodeSend, setCodeSend] = useState(false);
    const [isLoading, setLoading] = useState(false);


    const sendCode = async (email: string): Promise<void> => {
        setCodeSend(true);
        setLoading(true);
        try {
            await sessionApi.sendCode(email);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось отправить код");

            setCodeSend(false);
        } finally {
            setLoading(false);
        }
    };

    const verify = async (email: string, code: string): Promise<void> => {
        setLoading(true);
        try {
            const tokens = await sessionApi.verify(email, code);
            setTokens(tokens.accessToken, tokens.refreshToken);
            setCodeSend(false);
        } catch (err) {
            const apiError: ApiError = toApiError(err);
            notify("error", apiError.message || "Не удалось верифицировать код");
        } finally {
            setLoading(false);
        }
    };

    const demo = async (email: string): Promise<void> => {
        setLoading(true);
        try {
            const tokens = await sessionApi.demo(email);
            setTokens(tokens.accessToken, tokens.refreshToken);
            setCodeSend(false);
        } catch (err) {
            const apiError: ApiError = toApiError(err);
            notify("error", apiError.message || "Не удалось осуществить демо-вход");
        } finally {
            setLoading(false);
        }
    }

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
            const apiError: ApiError = toApiError(err);
            notify("error", apiError.message || "Не удалось выйти из сессии");
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
            const apiError: ApiError = toApiError(err);
            notify("error", apiError.message || "Не удалось выйти из всех сессий");
        } finally {
            clearSession();
            setCodeSend(false);
            setLoading(false);
        }
    };

    return {
        isCodeSend,
        isLoading,
        sendCode,
        verify,
        demo,
        logout,
        logoutAll,
    };
};