import type {CatalogButtonProps} from "@/widgets/header/ui/catalog-button/CatalogButton.types.ts";
import {Button} from "@/shared/ui/button";
import CatalogIcon from "@/shared/assets/icons/catalog.svg?react";


export const CatalogButton = (
    {
        displayMode = "full",
        ...props
    }: CatalogButtonProps
) => {
    const isCompact = displayMode === "compact";

    return (
        <Button
            {...props}
            variant="primary"
            size="medium"
            icon={<CatalogIcon/>}>
            {!isCompact && "Каталог"}
        </Button>
    );
}