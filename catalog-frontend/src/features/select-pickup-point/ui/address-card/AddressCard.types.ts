import type {PickupPoint} from "@/entities/pickup-point";


export interface AddressCardProps {
    deliveryAddress: PickupPoint,
    selected?: boolean,
    onSelect?: (id: string) => void
    className?: string,
}