import {AppRoutes} from "./routes/AppRoutes.tsx";
import {CartSync} from "./providers/CartSync.tsx";
import {WishlistSync} from "./providers/WishlistSync.ts";


function App() {
    return (
        <>
            <CartSync/>
            <WishlistSync/>
            <AppRoutes/>
        </>
    );
}

export default App
