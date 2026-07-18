import {useState} from "react";


export const useAuthModal = (isAuthenticated : boolean) => {
    const [isOpen, setIsOpen] = useState(false);

    const open = () => {

        console.log("Method was called")
        console.log(isAuthenticated);

        if (!isAuthenticated) {
            setIsOpen(true);
            console.log('Modal open');
        }
    }

    return {
        isOpen,
        open,
        close: () => setIsOpen(false),
    }
}