import type {DeliveryAddress} from "@/entities/delivery-address";


export interface AddressItemProps {
    deliveryAddress: DeliveryAddress,
    selected?: boolean,
    onSelect?: (id: string) => void
    className?: string,
}