import {useDeliveryLocation} from "@/features/delivery-address/model/useDeliveryLocation.ts";
import {AddressButton} from "@/features/delivery-address/ui/address-button/AddressButton.tsx";
import {AddressModal} from "@/features/delivery-address/ui/address-modal/AddressModal.tsx";


export const DeliveryAddress = () => {
    const {isOpen, selectedAddress, open, close, selectAddress} = useDeliveryLocation();

    return (
        <>
            <AddressButton
                address={selectedAddress ?? undefined}
                onClick={open}
            />
            <AddressModal isOpen={isOpen} onClose={close}/>
        </>
    )
}