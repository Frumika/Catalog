import type {CatalogButtonProps} from "@/widgets/header/ui/catalog-button/CatalogButton.types.ts";
import {Button} from "@/shared/ui/button";
import CatalogIcon from "@/shared/assets/icons/catalog.svg?react";
import {useNotify} from "@/shared/lib";


export const CatalogButton = (
    {
        displayMode = "full",
        ...props
    }: CatalogButtonProps
) => {
    const isCompact = displayMode === "compact";
    const notify = useNotify();

    return (
        <Button
            {...props}
            variant="primary"
            size="medium"
            icon={<CatalogIcon/>}
            onClick={() => notify("warning", "Каталог пока не реализован")}>
            {!isCompact && "Каталог"}
        </Button>
    );
}