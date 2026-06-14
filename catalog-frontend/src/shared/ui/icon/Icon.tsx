import type { ReactNode } from 'react';
import styles from './Icon.module.css';

interface IconProps {
    children: ReactNode;
    size?: 16 | 24;
}

export const Icon = ({ children, size = 16 }: IconProps) => (
    <span className={styles[`size${size}`]}>{children}</span>
);