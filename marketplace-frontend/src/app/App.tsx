import {AppRoutes} from "./routes/AppRoutes.tsx";
import {CartSync} from "./providers/CartSync.tsx";
import {WishlistSync} from "./providers/WishlistSync.tsx";
import {PickupPointSync} from "./providers/PickupPointSync.tsx";
import {NotificationContainer} from "@/widgets/notification-container/ui/NotificationContainer.tsx";
import {SessionSync} from "./providers/SessionSync.tsx";


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
