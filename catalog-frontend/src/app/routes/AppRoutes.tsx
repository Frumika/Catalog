import {Route, Routes} from 'react-router-dom';
import {HomePage} from "@/pages/home";
import {CartPage} from "@/pages/cart";


export const AppRoutes = () => {
    return (
        <Routes>
            <Route path="/" element={<HomePage/>}/>
            <Route path="/cart" element={<CartPage/>}/>
        </Routes>
    );
}