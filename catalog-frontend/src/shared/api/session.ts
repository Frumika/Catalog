const SESSION_KEY = "user_session_id";

export const session = {
    get(): string | null {
        // return localStorage.getItem(SESSION_KEY);
        return "a6837586-26af-4285-8f39-4f479e00bc93";
    },

    set(id: string): void {
        localStorage.setItem(SESSION_KEY, id);
    },

    clear(): void {
        localStorage.removeItem(SESSION_KEY);
    },
}