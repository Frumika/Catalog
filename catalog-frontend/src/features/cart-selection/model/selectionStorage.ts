import type {CartPosition} from "@/entities/cart";


const STORAGE_KEY = "cart-selection-storage";

export const selectionStorage = {

    getSavedIds(): number[] | null {
        try {
            const raw = localStorage.getItem(STORAGE_KEY);
            return raw !== null ? JSON.parse(raw) : null;
        } catch {
            return null;
        }
    },

    saveIds(productIds: number[]): void {
        try {
            localStorage.setItem(STORAGE_KEY, JSON.stringify(productIds));
        } catch (e) {
            console.error('Ошибка записи в localStorage', e);
        }
    },

    syncPositions(cartPositions: CartPosition[]): number[] {
        const currentIds = cartPositions.map((cp) => cp.productId);
        const savedIds = this.getSavedIds();

        if (savedIds === null) {
            this.saveIds(currentIds);
            return currentIds;
        }

        const validSelected = savedIds.filter((savedId) =>
            currentIds.includes(savedId)
        );

        this.saveIds(validSelected);
        return validSelected;
    },

    togglePosition(productId: number): number[] {
        const savedIds = this.getSavedIds() ?? [];
        const exists = savedIds.includes(productId);

        const updated = exists
            ? savedIds.filter((id) => id !== productId)
            : [...savedIds, productId];

        this.saveIds(updated);
        return updated;
    },


}

