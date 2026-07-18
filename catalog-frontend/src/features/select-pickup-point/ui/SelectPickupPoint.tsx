import {AddressButton} from "./address-button/AddressButton.tsx";
import {AddressModal} from "./address-modal/AddressModal.tsx";
import {usePickupPoint} from "@/entities/pickup-point";
import {useAddressModal} from "@/features/select-pickup-point/model/useAddressModal.ts";


export const SelectPickupPoint = () => {
    const {
        addresses,
        currentAddress,
        selectAddress,
        deleteAddress,
    } = usePickupPoint();

    const {
        isOpen,
        open,
        close,
    } = useAddressModal();

    return (
        <>
            <AddressButton
                address={currentAddress?.address}
                onClick={open}
            />
            <AddressModal
                isOpen={isOpen}
                onClose={close}
                addresses={addresses}
                selectedAddress={currentAddress}
                onSelect={selectAddress}
                onRemove={deleteAddress}
            />
        </>
    );
}