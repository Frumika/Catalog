import {useEffect, useState} from "react";
import type {PickupPoint} from "./PickupPoint.types.ts";
import {pickupPointApi} from "../api/pickupPointApi.ts";
import {ApiError, toApiError} from "@/shared/api";
import {useIsAuthenticated} from "@/entities/session";


export const usePickupPoint = () => {
    const [addresses, setAddresses] = useState<PickupPoint[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<ApiError | null>(null);

    const isAuthenticated = useIsAuthenticated();

    useEffect(() => {
        if (!isAuthenticated) {
            setAddresses([]);
            return;
        }

        setIsLoading(true);
        pickupPointApi
            .getAll()
            .then(address => setAddresses(address))
            .catch(error => setError(toApiError(error)))
            .finally(() => setIsLoading(false));

    }, [isAuthenticated]);

    const selectAddress = async (id: string) => {
        const sort = (arr: PickupPoint[]) =>
            [...arr].sort((a, b) => {
                if (!a.selectedAt) return 1;
                if (!b.selectedAt) return -1;
                return new Date(b.selectedAt).getTime() - new Date(a.selectedAt).getTime();
            });

        const previous = addresses;

        setAddresses(prev =>
            sort(prev.map(address => ({
                ...address,
                selectedAt: address.id === id ? new Date().toISOString() : address.selectedAt,
            })))
        );

        try {
            const updated = await pickupPointApi.select(id);
            setAddresses(prev => sort(prev.map(a => a.id === id ? updated : a)));
        } catch (error) {
            setError(toApiError(error));
            setAddresses(previous);
        }
    };

    const deleteAddress = async (id: string) => {
        const previous = addresses;

        setAddresses(prev => prev.filter(a => a.id !== id));

        try {
            await pickupPointApi.delete(id);
        } catch (error) {
            setError(toApiError(error));
            setAddresses(previous);
        }
    };

    const currentAddress = addresses[0]?.selectedAt ? addresses[0] : null;

    return {
        addresses,
        currentAddress,
        isLoading,
        error,
        selectAddress,
        deleteAddress,
    };
}