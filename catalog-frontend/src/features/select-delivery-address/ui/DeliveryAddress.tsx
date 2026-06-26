import {useDeliveryAddress} from "@/features/select-delivery-address/model/useDeliveryAddress.ts";
import {MOCK_ADDRESSES} from "@/features/select-delivery-address/model/addresses.ts";
import {AddressButton} from "@/features/select-delivery-address/ui/address-button/AddressButton.tsx";
import {AddressModal} from "@/features/select-delivery-address/ui/address-modal/AddressModal.tsx";


export const DeliveryAddress = () => {
    const {isOpen, selectedAddressId, open, close, selectAddress} = useDeliveryAddress();

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