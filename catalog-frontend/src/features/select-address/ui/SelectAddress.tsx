import {MOCK_ADDRESSES} from "../model/addresses.ts";
import {AddressButton} from "./address-button/AddressButton.tsx";
import {AddressModal} from "./address-modal/AddressModal.tsx";
import {useSelectAddress} from "../model/useSelectAddress.ts";


export const SelectAddress = () => {
    const {isOpen, selectedAddressId, open, close, selectAddress} = useSelectAddress();

    const selectedPoint = MOCK_ADDRESSES.find(
        point => point.id === selectedAddressId
    );
    const displayAddress = selectedPoint?.address;

    return (
        <>
            <AddressButton
                address={displayAddress}
                onClick={open}
            />
            <AddressModal
                isOpen={isOpen}
                onClose={close}
                onSelect={selectAddress}
                selectedAddressId={selectedAddressId}
            />
        </>
    )
}