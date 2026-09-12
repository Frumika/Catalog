import {useIsAuthenticated} from "@/entities/session";
import {usePickupPointSync} from "@/entities/pickup-point";


export const PickupPointSync = () => {
    const isAuthenticated = useIsAuthenticated();
    usePickupPointSync(isAuthenticated);
    return null;
}