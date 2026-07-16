export {useWishlistActions} from "./model/useWishlistActions.ts";
export {useWishlistSync} from "./model/useWishlistSync.ts";
export type {WishedProduct, Wishlist} from "./model/types.ts";

export {
    useWishlist,
    useIsProductWished,
    useWishlistTotalQuantity,
    useSetWishedProducts,
    useAddWishedProduct,
    useRemoveWishedProduct,
    useClearWishlist,
} from "./model/wishlistStore.ts";