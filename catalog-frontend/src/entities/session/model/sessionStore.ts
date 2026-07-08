import {create} from 'zustand';
import type {Session} from "@/entities/session";
import {localSessionStorage} from "@/shared/api";


type SessionState = {
    sessionId: string | null;
    email: string | null;
    login: string | null;
    isAuthenticated: boolean;
};

type SessionActions = {
    setUser: (sessionId: string | null, user: Session) => void;
    clearUser: () => void;
};

export const useSessionStore = create<SessionState & {
    setSession: (session: Session) => void;
    clearSession: () => void
}>((set) => ({
    sessionId: localSessionStorage.get(),
    email: null,
    login: null,
    isAuthenticated: !!localSessionStorage.get(),

    setSession: (dto) => {
        localSessionStorage.set(dto.sessionId);
        set({sessionId: dto.sessionId, email: dto.email, login: dto.login, isAuthenticated: true});
    },

    clearSession: () => {
        localSessionStorage.clear();
        set({sessionId: null, email: null, login: null, isAuthenticated: false});
    },
}));