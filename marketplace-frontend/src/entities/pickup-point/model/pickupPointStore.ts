import type {PickupPoint} from "@/entities/pickup-point";
import {create} from "zustand";


interface PickupPointState {
    pickupPoints: PickupPoint[];
}

interface PickupPointActions {
    setPickupPoints: (pickupPoints: PickupPoint[]) => void;
    addPickupPoint: (pickupPoint: PickupPoint) => void;
    selectPickupPoint: (updated: PickupPoint) => void;
    removePickupPoint: (pickupPoint: PickupPoint) => void;
    clearPickupPointStore: () => void;
}


const sortBySelectedAt = (arr: PickupPoint[]) =>
    [...arr].sort((a, b) => {
        if (!a.selectedAt) return 1;
        if (!b.selectedAt) return -1;
        return new Date(b.selectedAt).getTime() - new Date(a.selectedAt).getTime();
    });


const usePickupPointStore = create<PickupPointState & PickupPointActions>((set) => ({
    pickupPoints: [],

    setPickupPoints: (pickupPoints: PickupPoint[]): void => set({pickupPoints}),

    addPickupPoint: (pickupPoint: PickupPoint): void => set((state) => {
        const exist = state.pickupPoints.some(pp => pp.id === pickupPoint.id);

        return {
            pickupPoints: !exist ? [...state.pickupPoints, pickupPoint] : state.pickupPoints,
        };
    }),

    selectPickupPoint: (updated: PickupPoint) => set((state) => ({
        pickupPoints: sortBySelectedAt(
            state.pickupPoints.map(pp => (pp.id === updated.id ? updated : pp))
        ),
    })),

    removePickupPoint: (pickupPoint: PickupPoint): void => set((state) => {
        const exist = state.pickupPoints.some(pp => pp.id === pickupPoint.id);

        return {
            pickupPoints: exist ? state.pickupPoints.filter(pp => pp.id != pickupPoint.id) : state.pickupPoints,
        };
    }),

    clearPickupPointStore: (): void => set({pickupPoints: []}),
}));


export const usePickupPoints = () =>
    usePickupPointStore(state => state.pickupPoints);

export const useCurrentPickupPoint = () =>
    usePickupPointStore(state => state.pickupPoints[0]?.selectedAt ? state.pickupPoints[0] : null);

export const useSetPickupPoints = () =>
    usePickupPointStore(state => state.setPickupPoints);

export const useAddPickupPoint = () =>
    usePickupPointStore(state => state.addPickupPoint);

export const useSelectPickupPoint = () =>
    usePickupPointStore(state => state.selectPickupPoint);

export const useRemovePickupPoint = () =>
    usePickupPointStore(state => state.removePickupPoint);

export const useClearPickupPointsStore = () =>
    usePickupPointStore(state => state.clearPickupPointStore);