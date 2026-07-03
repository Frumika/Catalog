import {AppRoutes} from "@/app/routes/AppRoutes.tsx";
import {Header} from "@/widgets/header";
import {ProductList} from "@/widgets/product-list";


function App() {


    return (
        <main>
            <Header/>
            <ProductList/>
            <AppRoutes/>
        </main>
    )
}

export default App
