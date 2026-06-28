import type {PickupPoint} from "@/entities/pickup-point/model/PickupPoint.types.ts";


const BASE_URL = '/api/pickup-point';

export const PickupPointApi = {

    getAll: async (): Promise<PickupPoint[]> => {
        const response = await fetch(BASE_URL);
        if (!response.ok) throw new Error('Не удалось загрузить адреса');
        return response.json();
    },

    select: async (id: string): Promise<PickupPoint> => {
        const response = await fetch(`${BASE_URL}/select/${id}`, {method: 'PATCH'});
        if (!response.ok) throw new Error('Не удалось выбрать адрес');
        return response.json();
    },

    delete: async (id: string): Promise<void> => {
        const response = await fetch(`${BASE_URL}/${id}`, {method: 'DELETE'});
        if (!response.ok) throw new Error('Не удалось удалить адрес');
    },

    add: async (address: string): Promise<PickupPoint> => {
        const res = await fetch(BASE_URL, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({address}),
        });
        if (!res.ok) throw new Error('Не удалось добавить адрес');
        return res.json();
    },
}