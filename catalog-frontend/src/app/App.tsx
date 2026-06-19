import {AppRoutes} from "@/app/routes/routes.tsx";
import {Logo} from "@/shared/ui/logo";
import {Button} from "@/shared/ui/button";
import {NavButton} from "@/widgets/header/ui/nav-button";
import {SearchBar} from "@/features/search/search-bar";
import CatalogIcon from "@/shared/assets/catalog.svg?react";
import ProfileIcon from "@/shared/assets/profile.svg?react";
import OrderIcon from "@/shared/assets/order.svg?react";
import WishIcon from "@/shared/assets/wish.svg?react";
import CartIcon from "@/shared/assets/cart.svg?react";
import {useState} from "react";


function App() {
    const [query, setQuery] = useState('');

    return (
        <main>
            <h1>Каталог продукции</h1>


            <SearchBar
                value={query}
                placeholder={"Ищите на Wildboars"}
                onChange={setQuery}
                onSearch={() => console.log(query)}
                onClear={() => setQuery("")}
            />

            <Button
                variant="primary"
                size="medium"
                icon={<CatalogIcon/>}>
                Каталог
            </Button>

            <NavButton
                icon={<ProfileIcon/>}>
                Войти
            </NavButton>

            <NavButton
                icon={<OrderIcon/>}
                badgeValue={100}>
                Заказы
            </NavButton>

            <NavButton
                icon={<WishIcon/>}
                badgeValue={5}>
                Избранное
            </NavButton>

            <NavButton
                icon={<CartIcon/>}
                badgeValue={10}>
                Корзина
            </NavButton>


            <Logo disabled={false}
                  onClick={() => console.log("Logo was clicked")}
            />

            <AppRoutes/>
        </main>
    )
}

export default App
