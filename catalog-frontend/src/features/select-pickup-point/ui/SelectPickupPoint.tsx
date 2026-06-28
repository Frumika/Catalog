import {AddressButton} from "./address-button/AddressButton.tsx";
import {AddressModal} from "./address-modal/AddressModal.tsx";
import {useSelectPickupPoint} from "../model/useSelectPickupPoint.ts";


export const SelectPickupPoint = () => {
    const {
        isOpen,
        addresses,
        currentAddress,
        open,
        close,
        selectAddress,
        deleteAddress,
    } = useSelectPickupPoint();

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