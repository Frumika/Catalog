import {AppRoutes} from "@/app/routes/AppRoutes.tsx";
import {Header} from "@/widgets/header";
import {ProductCard} from "@/entities/product";
import type {ProductPreview} from "@/entities/product/model/Product.types.ts";


function App() {
    const product1: ProductPreview = {
        productId: 1,
        productName: "iPhone 15 Pro",
        price: 1199.99,
        discountPrice: 240,
        discountPercent: 80,
        reviewCount: 5,
        averageScore: 4.4,
        imageUrl: "images/iphone_15_pro_1.png",
    }

    const product2: ProductPreview = {
        ...product1,
        productId: 2,
        productName: "Iphone 20432 Pro Ultra Max Exp Many text here was added"
    }

    return (
        <main>
            <Header/>
            <ProductCard product={product1}/>
            <ProductCard product={product2}/>
            <AppRoutes/>
        </main>
    )
}

export default App
