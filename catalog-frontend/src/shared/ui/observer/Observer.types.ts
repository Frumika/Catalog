import type {ReactNode} from 'react';

export interface ObserverProps {
    onIntersect: () => void;
    rootMargin?: string;
    className?: string;
    children?: ReactNode;
}