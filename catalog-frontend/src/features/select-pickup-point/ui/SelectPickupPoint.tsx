import {AddressButton} from "./address-button/AddressButton.tsx";
import {AddressModal} from "./address-modal/AddressModal.tsx";
import {useAddressModal} from "@/features/select-pickup-point/model/useAddressModal.ts";


export const SelectPickupPoint = () => {
    const {
        isOpen,
        open,
        close,
    } = useAddressModal();

    return (
        <>
            <AddressButton onClick={open}/>

            <AddressModal isOpen={isOpen} onClose={close}/>
        </>
    );
}