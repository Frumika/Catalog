import {useState} from "react";


export const useDeliveryLocation = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedAddress, setSelectedAddress] = useState<string | null>(null);

    return {
        isOpen,
        selectedAddress,
        open:  () => setIsOpen(true),
        close: () => setIsOpen(false),
        selectAddress: (address: string) => {
            setSelectedAddress(address);
            setIsOpen(false);
        },
    };
};