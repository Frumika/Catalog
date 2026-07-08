const SESSION_KEY = "user_session_id";

export const localSessionStorage = {
    get(): string | null {
        return localStorage.getItem(SESSION_KEY);
    },

    set(id: string): void {
        localStorage.setItem(SESSION_KEY, id);
    },

    clear(): void {
        localStorage.removeItem(SESSION_KEY);
    },
}