import {useEffect, useState} from "react";
import type {User} from "./types.ts";
import {userApi} from "../api/userApi.ts";
import {ApiError, session, toApiError} from "@/shared/api";


export const useUser = () => {
    const [user, setUser] = useState<User | null>(null);
    const [isCodeSend, setCodeSend] = useState(false);
    const [isLoading, setLoading] = useState(true);
    const [error, setError] = useState<ApiError | null>(null);

    const isVerify = user !== null;

    useEffect(() => {
        const sessionId: string | null = session.get();
        if (!sessionId) {
            setLoading(false);
            return;
        }

        userApi.getUser(sessionId)
            .then(user => setUser(user))
            .catch(error => setError(toApiError(error)))
            .finally(() => setLoading(false));
    }, []);

    const sendCode = async (email: string): Promise<void> => {
        try {
            await userApi.sendCode(email);
            setError(null);
            setCodeSend(true);
        } catch (error) {
            setError(toApiError(error));
            setCodeSend(false);
        }
    };

    const verify = async (email: string, code: string): Promise<void> => {
        try {
            const user = await userApi.verify(email, code);
            session.set(user.sessionId);
            setError(null);
            setCodeSend(false);
            setUser(user);
        } catch (error) {
            setError(toApiError(error));
        }
    };

    const logout = async (): Promise<void> => {
        const sessionId = session.get();
        if (!sessionId) {
            return;
        }
        try {
            await userApi.logout(sessionId);
        } catch (error) {
            setError(toApiError(error));
        } finally {
            session.clear();
            setUser(null);
            setCodeSend(false);
        }
    };

    const logoutAll = async (): Promise<void> => {
        const sessionId = session.get();
        if (!sessionId) {
            return;
        }
        try {
            await userApi.logoutAll(sessionId);
        } catch (error) {
            setError(toApiError(error));
        } finally {
            session.clear();
            setUser(null);
            setCodeSend(false);
        }
    };

    return {
        user,
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