import type {PickupPoint} from "@/entities/pickup-point";

export interface AddressModalProps {
    addresses?: PickupPoint[];
    selectedAddress: PickupPoint | null;
    isOpen: boolean;
    onClose: () => void;
    onSelect: (id: string) => void;
    onRemove: (id: string) => void;
}