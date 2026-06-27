import {useState} from "react";
import {useDeliveryAddress} from "@/entities/delivery-address";


export const useSelectAddress = () => {
    const [isOpen, setIsOpen] = useState(false);
    const {
        addresses,
        currentAddress,
        error,
        selectAddress,
        deleteAddress,
    } = useDeliveryAddress();

    return {
        isOpen,
        addresses,
        currentAddress,
        error,
        open:  () => setIsOpen(true),
        close: () => setIsOpen(false),
        selectAddress: async (id: string) => await selectAddress(id),
        deleteAddress,
    };
};