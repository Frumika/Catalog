import {AppRoutes} from "./routes/AppRoutes.tsx";
import {CartSync} from "@/app/sync/CartSync.tsx";
import {WishlistSync} from "@/app/sync/WishlistSync.tsx";
import {PickupPointSync} from "@/app/sync/PickupPointSync.tsx";
import {SessionSync} from "@/app/sync/SessionSync.tsx";
import {NotificationContainer} from "@/shared/ui/notification-container";


function App() {
    return (
        <>
            <SessionSync/>
            <CartSync/>
            <WishlistSync/>
            <PickupPointSync/>
            <AppRoutes/>
            <NotificationContainer/>
        </>
    );
}

export default App
