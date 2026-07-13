import {AppRoutes} from "./routes/AppRoutes.tsx";
import {CartSync} from "./providers/CartSync.tsx";


function App() {
    return (
        <>
            <CartSync/>
            <AppRoutes/>
        </>
    );
}

export default App
