import {useIsAuthenticated} from "@/entities/session";
import {useWishlistSync} from "@/entities/wishlist";


export const WishlistSync = () => {
    const isAuthenticated = useIsAuthenticated();
    useWishlistSync(isAuthenticated);
    return null;
}