export interface SortOption {
    id: number;
    label: string;
}

export const SORT_OPTIONS: SortOption[] = [
    {id: 1, label: 'Новые'},
    {id: 2, label: 'Старые'},
    {id: 3, label: 'Дешёвые'},
    {id: 4, label: 'Дорогие'},
    {id: 5, label: 'С большими скидками'}
];