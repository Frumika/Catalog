import {AddressButton} from "./address-button/AddressButton.tsx";
import {AddressModal} from "./address-modal/AddressModal.tsx";
import {useDisclosure} from "@/shared/lib";


export const SelectPickupPoint = () => {

    const {isOpen, open, close} = useDisclosure();

    return (
        <>
            <AddressButton onClick={open}/>

            <AddressModal isOpen={isOpen} onClose={close}/>
        </>
    );
}