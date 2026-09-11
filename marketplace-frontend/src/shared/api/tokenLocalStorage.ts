const REFRESH_TOKEN_KEY = "refresh_token";
const ACCESS_TOKEN_KEY = "access_token";


type Listener = () => void;
const listeners = new Set<Listener>();
const notify = () => listeners.forEach((listener) => listener());

export const tokenLocalStorage = {
    getRefreshToken(): string | null {
        return localStorage.getItem(REFRESH_TOKEN_KEY);
    },

    setRefreshToken(refreshToken: string): void {
        localStorage.setItem(REFRESH_TOKEN_KEY, refreshToken);
        notify();
    },

    clearRefreshToken(): void {
        localStorage.removeItem(REFRESH_TOKEN_KEY);
        notify();
    },

    getAccessToken(): string | null {
        return localStorage.getItem(ACCESS_TOKEN_KEY);
    },

    setAccessToken(accessToken: string): void {
        localStorage.setItem(ACCESS_TOKEN_KEY, accessToken);
        notify();
    },

    clearAccessToken(): void {
        localStorage.removeItem(ACCESS_TOKEN_KEY);
        notify();
    },

    clearStorage(): void {
        localStorage.removeItem(REFRESH_TOKEN_KEY);
        localStorage.removeItem(ACCESS_TOKEN_KEY);
        notify();
    },

    subscribe: (listener: Listener) => {
        listeners.add(listener);
        return () => listeners.delete(listener);
    },
}