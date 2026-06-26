import {useState} from "react";


export const useDeliveryLocation = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedAddressId, setSelectedAddressId] = useState<string | null>(null);

    return {
        isOpen,
        selectedAddressId,
        open: () => setIsOpen(true),
        close: () => setIsOpen(false),
        selectAddress: (id: string) => setSelectedAddressId(id),
    };
};