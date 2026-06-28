import type {PickupPoint} from "@/entities/pickup-point";


export interface AddressItemProps {
    deliveryAddress: PickupPoint,
    selected?: boolean,
    onSelect?: (id: string) => void
    className?: string,
}