export interface AddressItemProps {
    id: string,
    address: string,
    shelfLife: number,
    selected?: boolean,
    onSelect?: (id: string) => void
    className?: string,
}