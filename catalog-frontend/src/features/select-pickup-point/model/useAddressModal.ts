import {useState} from "react";

export const useAddressModal = () => {
    const [isOpen, setIsOpen] = useState(false);

    return {
        isOpen,
        open:  () => setIsOpen(true),
        close: () => setIsOpen(false),
    }
}