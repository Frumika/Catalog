const SESSION_KEY = "user_session_id";

export const session = {
    get(): string | null {
        // return localStorage.getItem(SESSION_KEY);
        return "49b361fc-64fb-42d2-8f5f-489915312482";
    },

    set(id: string): void {
        localStorage.setItem(SESSION_KEY, id);
    },

    clear(): void {
        localStorage.removeItem(SESSION_KEY);
    },
}