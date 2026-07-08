import {useState} from "react";
import {useUserSession} from "@/entities/user-session";

export const useAuthModal = () => {
    const [isOpen, setIsOpen] = useState(false);
    const {
        session,
        isVerify,
        isCodeSend,
        sendCode,
        verify,
        logout,
        logoutAll
    } = useUserSession()

    const open = () => {
        if (!isVerify) {
            setIsOpen(true);
        }
    }

    return {
        isOpen,
        session,
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