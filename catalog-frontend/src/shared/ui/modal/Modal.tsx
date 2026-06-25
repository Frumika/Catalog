import {useEffect} from 'react';
import {createPortal} from 'react-dom';
import type {ModalProps} from './Modal.types';
import styles from './Modal.module.css';


export const Modal = (
    {
        isOpen,
        onClose,
        children,
        className
    }: ModalProps) => {

    useEffect(() => {
        if (!isOpen) return;

        document.body.style.overflow = 'hidden';

        return () => {
            document.body.style.overflow = '';
        };
    }, [isOpen]);

    if (!isOpen) return null;

    const modalStyles = [
        styles.modal,
        className
    ].filter(Boolean).join(' ');

    return createPortal(
        <div
            className={styles.overlay}
            onClick={onClose}
        >
            <div
                className={modalStyles}
                onClick={event => event.stopPropagation()}
            >
                {children}
            </div>
        </div>,
        document.body
    );
};