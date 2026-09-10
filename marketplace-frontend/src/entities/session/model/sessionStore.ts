import {create} from 'zustand';
import {tokenLocalStorage} from "@/shared/api";


interface SessionState {
    isAuthenticated: boolean;
}

interface SessionActions {
    setTokens: (accessToken: string, refreshToken: string) => void;
    clearSession: () => void;
}

export const useSessionStore = create<SessionState & SessionActions>((set) => ({
    isAuthenticated: !!tokenLocalStorage.getRefreshToken(),

    setTokens: (accessToken: string, refreshToken: string) => {
        tokenLocalStorage.setAccessToken(accessToken);
        tokenLocalStorage.setRefreshToken(refreshToken);
        set({isAuthenticated: true});
    },

    clearSession: () => {
        tokenLocalStorage.clearStorage();
        set({isAuthenticated: false});
    },
}));

// Селекторы
export const useIsAuthenticated = () => useSessionStore((s) => s.isAuthenticated);
export const useSetTokens = () => useSessionStore((s) => s.setTokens);
export const useClearSession = () => useSessionStore((s) => s.clearSession);