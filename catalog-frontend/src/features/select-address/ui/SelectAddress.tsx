import {AddressButton} from "./address-button/AddressButton.tsx";
import {AddressModal} from "./address-modal/AddressModal.tsx";
import {useSelectAddress} from "../model/useSelectAddress.ts";


export const SelectAddress = () => {
    const {
        isOpen,
        addresses,
        currentAddress,
        open,
        close,
        selectAddress,
        deleteAddress,
    } = useSelectAddress();

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
                onDelete={deleteAddress}
            />
        </>
    );
}