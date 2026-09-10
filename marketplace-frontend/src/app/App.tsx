import {AppRoutes} from "./routes/AppRoutes.tsx";
import {CartSync} from "./providers/CartSync.tsx";
import {WishlistSync} from "./providers/WishlistSync.tsx";
import {PickupPointSync} from "@/app/providers/PickupPointSync.tsx";
import {NotificationContainer} from "@/widgets/notification-container/ui/NotificationContainer.tsx";


function App() {
    return (
        <>
            <CartSync/>
            <WishlistSync/>
            <PickupPointSync/>
            <AppRoutes/>
            <NotificationContainer/>
        </>
    );
}

export default App
