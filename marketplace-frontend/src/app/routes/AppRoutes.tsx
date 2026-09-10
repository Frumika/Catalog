import {Route, Routes} from 'react-router-dom';
import {HomePage} from "@/pages/home";
import {CartPage} from "@/pages/cart";
import {WishlistPage} from "@/pages/wishlist";
import {CheckoutPage} from "@/pages/checkout";


export const AppRoutes = () => {
    return (
        <Routes>
            <Route path={"/"} element={<HomePage/>}/>
            <Route path={"/wishlist"} element={<WishlistPage/>}/>
            <Route path={"/cart"} element={<CartPage/>}/>
            <Route path={"/checkout"} element={<CheckoutPage/>}/>
        </Routes>
    );
}