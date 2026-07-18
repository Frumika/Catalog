import {type ReactNode, type RefObject, useEffect, useLayoutEffect, useRef, useState} from 'react';
import {createPortal} from 'react-dom';
import {computePosition, offset, flip, shift, type Placement} from '@floating-ui/dom';
import styles from './Popover.module.css';


type Position = { top: number; left: number } | null;

interface PopoverProps {
    isOpen: boolean;
    onClose: () => void;
    anchorRef: RefObject<HTMLElement>;
    children: ReactNode;
    className?: string;
    placement?: Placement;
}

export const Popover = (
    {
        isOpen,
        onClose,
        anchorRef,
        children,
        className,
        placement = 'bottom-end'
    }: PopoverProps) => {

    const popoverRef = useRef<HTMLDivElement>(null);
    const [position, setPosition] = useState<Position>(null);

    useLayoutEffect(() => {
        if (!isOpen) {
            setPosition(null);
            return;
        }

        const anchor = anchorRef.current;
        const floating = popoverRef.current;
        if (!anchor || !floating) return;

        computePosition(anchor, floating, {
            strategy: 'fixed', // координаты viewport-relative — без position:absolute и без обёртки-контейнера
            placement,
            middleware: [offset(8), flip(), shift({padding: 8})]
        }).then(({x, y}) => setPosition({top: y, left: x}));
    }, [isOpen, anchorRef, placement]);

    // закрытие по клику вовне
    useEffect(() => {
        if (!isOpen) return;

        const handleOutsideClick = (event: MouseEvent) => {
            const target = event.target as Node;
            if (popoverRef.current?.contains(target)) return;
            if (anchorRef.current?.contains(target)) return; // клик по якорю — не наша забота, им управляет владелец anchor'а
            onClose();
        };

        document.addEventListener('mousedown', handleOutsideClick);
        return () => document.removeEventListener('mousedown', handleOutsideClick);
    }, [isOpen, onClose, anchorRef]);

    useEffect(() => {
        if (!isOpen) return;

        const handleClose = () => onClose();
        window.addEventListener('scroll', handleClose, true);
        window.addEventListener('resize', handleClose);

        return () => {
            window.removeEventListener('scroll', handleClose, true);
            window.removeEventListener('resize', handleClose);
        };
    }, [isOpen, onClose]);

    if (!isOpen) return null;

    const popoverStyles = [styles.popover, className].filter(Boolean).join(' ');

    return createPortal(
        <div
            ref={popoverRef}
            className={popoverStyles}
            style={
                position
                    ? {top: position.top, left: position.left, visibility: 'visible'}
                    : {top: 0, left: 0, visibility: 'hidden'}
            }
        >
            {children}
        </div>,
        document.body
    );
};