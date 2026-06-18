import {AppRoutes} from "@/app/routes/routes.tsx";
import {Input} from "@/shared/ui/input";
import {Logo} from "@/shared/ui/logo";
import {Button} from "@/shared/ui/button";
import {NavButton} from "@/widgets/header/nav-button";
import {Icon} from "@/shared/ui/icon";

import CatalogIcon from "@/shared/assets/catalog.svg?react";
import ProfileIcon from "@/shared/assets/profile.svg?react";
import OrderIcon from "@/shared/assets/order.svg?react";
import WishIcon from "@/shared/assets/wish.svg?react";
import CartIcon from "@/shared/assets/cart.svg?react";


function App() {
    return (
        <main>
            <h1>Каталог продукции</h1>
            <Input
                onChange={(value) => console.log(value)}>
            </Input>

            <Button
                variant="primary"
                size="large"
                icon={<Icon><CatalogIcon/></Icon>}>
                Каталог
            </Button>

            <NavButton
                icon={<Icon><ProfileIcon/></Icon>}>
                Войти
            </NavButton>

            <NavButton
                icon={<Icon><OrderIcon/></Icon>}
                badgeValue={100}>
                Заказы
            </NavButton>

            <NavButton
                icon={<Icon><WishIcon/></Icon>}
                badgeValue={5}>
                Избранное
            </NavButton>

            <NavButton
                icon={<Icon><CartIcon/></Icon>}
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
