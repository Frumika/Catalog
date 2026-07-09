import {Route, Routes} from 'react-router-dom';
import {HomePage} from "@/pages/home";
import {CartPage} from "@/pages/cart";
import { useCartStore } from '@/entities/cart';
import { useEffect } from 'react';

export const AppRoutes = () => {
    const fetchCart = useCartStore(state => state.fetchCart);

    useEffect(() => {
        fetchCart(); // Загружаем корзину один раз при старте приложения
    }, [fetchCart]);
    return (
        <Routes>
            <Route path="/" element={<HomePage/>}/>
            {/*<Route path="/catalog" element={<CatalogPage/>}/>*/}
            <Route path="/cart" element={<CartPage/>}/>
        </Routes>
    );
}