export interface AddressItemProps {
    id: string,
    address: string,
    shelfLife: number,
    selected?: boolean,
    onClick?: () => void
    className?: string,
}