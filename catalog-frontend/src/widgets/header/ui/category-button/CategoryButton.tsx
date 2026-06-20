import type {CategoryButtonProps} from "@/widgets/header/ui/category-button/CategoryButton.types.ts";
import styles from "./CategoryButton.module.css";
import {Button} from "@/shared/ui/button";

export const CategoryButton = (
    {
        icon,
        children,
        className,
        ...props
    }: CategoryButtonProps) => {

    const categoryButtonStyles = [
        styles.categoryButton,
        className,
    ].filter(Boolean).join(' ');

    return (
        <Button
            {...props}
            className={categoryButtonStyles}
            variant="ghost"
            size={"small"}
            icon={icon}
        >
            {children}
        </Button>
    );
}