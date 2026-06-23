import type {CatalogButtonProps} from "@/widgets/header/ui/catalog-button/CatalogButton.types.ts";
import {Button} from "@/shared/ui/button";
import CatalogIcon from "@/shared/assets/catalog.svg?react";


export const CatalogButton = (
    {
        hideText = false
    }: CatalogButtonProps
) => {
    return (
        <Button
            variant="primary"
            size="medium"
            icon={<CatalogIcon/>}>
            {!hideText && "Каталог"}
        </Button>
    );
}