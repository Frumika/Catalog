export type BadgeVariant = 'dot' | 'value';

export interface BadgeProps {
    variant?: BadgeVariant;
    value?: number
    max?: number;
    visible?: boolean;
    className?: string;
}