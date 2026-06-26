export interface AddressModalProps {
    isOpen: boolean;
    onClose: () => void;
    onSelect: (id: string) => void;
    selectedAddressId: string | null;
    addresses?: string[];
}