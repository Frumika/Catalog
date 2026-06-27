import type {DeliveryAddress} from "@/entities/delivery-address";

export interface AddressModalProps {
    addresses?: DeliveryAddress[];
    selectedAddress: DeliveryAddress | null;
    isOpen: boolean;
    onClose: () => void;
    onSelect: (id: string) => void;
    onDelete: (id: string) => void;
}