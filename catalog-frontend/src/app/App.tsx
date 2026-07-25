import {AppRoutes} from "./routes/AppRoutes.tsx";
import {CartSync} from "./providers/CartSync.tsx";
import {WishlistSync} from "./providers/WishlistSync.tsx";
import {PickupPointSync} from "@/app/providers/PickupPointSync.tsx";


function App() {
    return (
        <>
            <CartSync/>
            <WishlistSync/>
            <PickupPointSync/>
            <AppRoutes/>
        </>
    );
}

export default App
