import type {CatalogButtonProps} from "@/widgets/header/ui/catalog-button/CatalogButton.types.ts";
import {Button} from "@/shared/ui/button";
import CatalogIcon from "@/shared/assets/icons/catalog.svg?react";


export const CatalogButton = (
    {
        hideText = false,
        ...props
    }: CatalogButtonProps
) => {
    return (
        <Button
            {...props}
            variant="primary"
            size="medium"
            icon={<CatalogIcon/>}>
            {!hideText && "Каталог"}
        </Button>
    );
}