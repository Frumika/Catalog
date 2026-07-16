import {Route, Routes} from 'react-router-dom';
import {HomePage} from "@/pages/home";
import {CartPage} from "@/pages/cart";
import {WishlistPage} from "@/pages/wishlist";



export const AppRoutes = () => {
    return (
        <Routes>
            <Route path="/" element={<HomePage/>}/>
            <Route path="/wishlist" element={<WishlistPage/>}/>
            <Route path="/cart" element={<CartPage/>}/>
        </Routes>
    );
}