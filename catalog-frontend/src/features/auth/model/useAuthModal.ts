import {useState} from "react";


export const useAuthModal = (isAuthenticated : boolean) => {
    const [isOpen, setIsOpen] = useState(false);

    const open = () => {
        if (!isAuthenticated) {
            setIsOpen(true);
        }
    }

    return {
        isOpen,
        open,
        close: () => setIsOpen(false),
    }
}