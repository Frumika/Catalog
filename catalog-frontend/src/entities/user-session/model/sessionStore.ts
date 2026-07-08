import {create} from 'zustand';
import type {UserSession} from "../model/types.ts";
import {localSessionStorage} from "@/shared/api";


interface SessionState {
    sessionId: string | null;
    email: string | null;
    login: string | null;
    isAuthenticated: boolean;
}

interface SessionActions {
    setSession: (session: UserSession) => void;
    clearSession: () => void;
}

export const useSessionStore = create<SessionState & SessionActions>((set) => ({
    sessionId: localSessionStorage.get(),
    email: null,
    login: null,
    isAuthenticated: !!localSessionStorage.get(),

    setSession: (userSession: UserSession) => {
        localSessionStorage.set(userSession.sessionId);
        set({
            sessionId: userSession.sessionId,
            email: userSession.email,
            login: userSession.login,
            isAuthenticated: true,
        });
    },

    clearSession: () => {
        localSessionStorage.clear();
        set({
            sessionId: null,
            email: null,
            login: null,
            isAuthenticated: false
        });
    },
}));


export const useIsAuthenticated = () => useSessionStore((s) => s.isAuthenticated);
export const useSessionId = () => useSessionStore((s) => s.sessionId);
export const useSessionEmail = () => useSessionStore((s) => s.email);
export const useSessionLogin = () => useSessionStore((s) => s.login);

export const useSetSession = () => useSessionStore((s) => s.setSession);
export const useClearSession = () => useSessionStore((s) => s.clearSession);