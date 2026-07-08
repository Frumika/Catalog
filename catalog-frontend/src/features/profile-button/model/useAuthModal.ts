import {useState} from "react";
import {useUser} from "@/entities/user";

export const useAuthModal = () => {
    const [isOpen, setIsOpen] = useState(false);
    const {
        user,
        isVerify,
        isCodeSend,
        sendCode,
        verify,
        logout,
        logoutAll
    } = useUser()

    const open = () => {
        if (!isVerify) {
            setIsOpen(true);
        }
    }

    return {
        isOpen,
        user,
        isVerify,
        isCodeSend,
        sendCode,
        verify,
        logout,
        logoutAll,
        open,
        close: () => setIsOpen(false),
    }
}