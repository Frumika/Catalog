import {Route, Routes} from 'react-router-dom';
import {HomePage} from "@/pages/home";
import {CartPage} from "@/pages/cart";
import {useCartStore} from '@/entities/cart';
import {useEffect} from 'react';
import {useIsAuthenticated} from "@/entities/session";

export const AppRoutes = () => {
    const isAuthenticated = useIsAuthenticated();
    const fetchCart = useCartStore(state => state.fetchCart);

    useEffect(() => {
        if (!isAuthenticated) {
            return;
        }

        void fetchCart();
    }, [isAuthenticated]);

    return (
        <Routes>
            <Route path="/" element={<HomePage/>}/>
            <Route path="/cart" element={<CartPage/>}/>
        </Routes>
    );
}