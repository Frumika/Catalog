import {useState} from "react";
import {usePickupPoint} from "@/entities/pickup-point";


export const useSelectPickupPoint = () => {
    const [isOpen, setIsOpen] = useState(false);
    const {
        addresses,
        currentAddress,
        error,
        selectAddress,
        deleteAddress,
    } = usePickupPoint();

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