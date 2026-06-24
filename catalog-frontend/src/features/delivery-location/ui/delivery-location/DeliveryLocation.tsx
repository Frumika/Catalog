import {useDeliveryLocation} from "../../model/useDeliveryLocation.ts";
import {DeliveryButton} from "../delivery-button/DeliveryButton.tsx";
import {DeliveryModal} from "../delivery-modal/DeliveryModal.tsx";


export const DeliveryLocation = () => {
    const {isOpen, selectedAddress, open, close, selectAddress} = useDeliveryLocation();

    return (
        <>
            <DeliveryButton
                address={selectedAddress ?? undefined}
                onClick={open}
            />
            <DeliveryModal isOpen={isOpen} onClose={close}/>
        </>
    )
}