import {AppRoutes} from "@/app/routes/AppRoutes.tsx";
import {Header} from "@/widgets/header";
import {ProductCard} from "@/entities/product";


function App() {
    return (
        <main>
            <Header />
            <ProductCard/>
            <AppRoutes/>
        </main>
    )
}

export default App
