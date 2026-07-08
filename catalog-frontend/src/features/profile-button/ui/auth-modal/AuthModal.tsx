import {Modal} from "@/shared/ui/modal";

interface AuthModalProps {
    isOpen: boolean;
    onClose?: () => void;
}

export const AuthModal = (
    {
        isOpen,
        onClose,
    }: AuthModalProps
) => {

    return (
        <Modal isOpen={isOpen} onClose={onClose}>

        </Modal>
    )
}