import type {ContentContainerProps} from "./ContentContainer.types.ts";
import styles from "./ContentContainer.module.css";


export const ContentContainer = (
    {
        children,
        className,
        ...props
    }: ContentContainerProps
) => {
    const containerClasses = [
        styles.container,
        className,
    ].filter(Boolean).join(' ');

    return (
        <div
            {...props}
            className={containerClasses}>
            {children}
        </div>
    );
}