import {useCartSync} from "@/entities/cart";
import {useIsAuthenticated} from "@/entities/session";


export const CartSync = () => {
    const isAuthenticated = useIsAuthenticated();
    useCartSync(isAuthenticated);
    return null;
};