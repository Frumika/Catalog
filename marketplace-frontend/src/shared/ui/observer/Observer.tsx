import {useEffect, useRef} from 'react';
import type {ObserverProps} from './Observer.types';

export const Observer = (
    {
        onIntersect,
        rootMargin = '200px',
        className,
        children
    }: ObserverProps) => {

    const targetRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const target = targetRef.current;
        if (!target) return;

        const observer = new IntersectionObserver(
            ([entry]) => {

                if (entry.isIntersecting) {
                    onIntersect();
                }
            },
            {rootMargin}
        );

        observer.observe(target);
        
        return () => {
            observer.disconnect();
        };
    }, [onIntersect, rootMargin]);

    return (
        <div ref={targetRef} className={className}>
            {children}
        </div>
    );
};