import {useDeliveryLocation} from "@/features/delivery-address/model/useDeliveryLocation.ts";
import {AddressButton} from "@/features/delivery-address/ui/address-button/AddressButton.tsx";
import {AddressModal} from "@/features/delivery-address/ui/address-modal/AddressModal.tsx";
import {MOCK_ADDRESSES} from "@/features/delivery-address/model/addresses.ts";


export const DeliveryAddress = () => {
    const {isOpen, selectedAddressId, open, close, selectAddress} = useDeliveryLocation();

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