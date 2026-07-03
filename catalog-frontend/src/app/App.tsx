import {AppRoutes} from "@/app/routes/AppRoutes.tsx";
import {Header} from "@/widgets/header";
import type {ProductPreview} from "@/entities/product/model/Product.types.ts";
import {ProductCard} from "@/entities/product";


function App() {
    const product1: ProductPreview = {
        productId: 1,
        productName: "iPhone 15 Pro",
        price: 1199.99,
        discountPrice: 240,
        discountPercent: 80,
        reviewCount: 5,
        averageScore: 4.4,
        imageUrl: "http://localhost:8000/images/iphone_15_pro_1.png",
    }

    const product2: ProductPreview = {
        ...product1,
        productId: 2,
        productName: "Iphone 20432 Pro Ultra Max Exp Many text here was added"
    }

    return (
        <main>
            <Header/>
            <div>
                <ProductCard product={product1}/>
                <ProductCard product={product2}/>
            </div>

            <AppRoutes/>
        </main>
    )
}

export default App
