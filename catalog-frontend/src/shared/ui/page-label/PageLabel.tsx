import styles from "./PageLabel.module.css";


interface PageLabelProps {
    title: string;
    quantity: number;
    className?: string;
}

export const PageLabel = (
    {
        title,
        quantity,
        className,
    }: PageLabelProps
) => {

    const pageLabelStyles = [
        styles.pageLabel,
        className,
    ].filter(Boolean).join(' ');

    return (
        <div className={pageLabelStyles}>
            <h2 className={styles.title}>{title}</h2>
            <span className={styles.count}>{quantity}</span>
        </div>
    );
}