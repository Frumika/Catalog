import {useEffect, useState} from "react";
import type {DeliveryAddress} from "@/entities/delivery-address/model/DeliveryAddress.types.ts";
import {DeliveryAddressApi} from "@/entities/delivery-address/api/deliveryAddressApi.ts";

export const useDeliveryAddress = () => {
    const [addresses, setAddresses] = useState<DeliveryAddress[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        setIsLoading(true);
        DeliveryAddressApi
            .getAll()
            .then(address => setAddresses(address))
            .catch(error => setError(error))
            .finally(() => setIsLoading(false));
    }, []);

    const selectAddress = async (id: string) => {
        const sort = (arr: DeliveryAddress[]) =>
            [...arr].sort((a, b) => {
                if (!a.selectedAt) return 1;
                if (!b.selectedAt) return -1;
                return new Date(b.selectedAt).getTime() - new Date(a.selectedAt).getTime();
            });

        setAddresses(prev =>
            sort(prev.map(address => ({
                ...address,
                selectedAt: address.id === id ? new Date().toISOString() : address.selectedAt,
            })))
        );

        try {
            const updated = await DeliveryAddressApi.select(id);
            setAddresses(prev => sort(prev.map(a => a.id === id ? updated : a)));
        } catch (e) {
            setError('Не удалось выбрать адрес');
        }
    };

    const deleteAddress = async (id: string) => {
        setAddresses(prev => prev.filter(a => a.id !== id));

        try {
            await DeliveryAddressApi.delete(id);
        } catch (e) {
            setError('Не удалось удалить адрес');
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