import {AppRoutes} from "@/app/routes.tsx";
import {Input} from "@/shared/ui/input";
import {Logo} from "@/shared/ui/logo";
import {Button} from "@/shared/ui/button";
import {NavButton} from "@/shared/ui/nav-button";
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
                icon={
                    <Icon size={"medium"}>
                        <CatalogIcon/>
                    </Icon>}>
                Каталог
            </Button>

            <NavButton
                icon={
                    <Icon size={"medium"}>
                        <ProfileIcon/>
                    </Icon>}>
                Войти
            </NavButton>

            <NavButton
                icon={
                    <Icon size={"medium"}>
                        <OrderIcon/>
                    </Icon>}>
                Заказы
            </NavButton>

            <NavButton
                icon={
                    <Icon size={"medium"}>
                        <WishIcon/>
                    </Icon>}>
                Избранное
            </NavButton>

            <NavButton
                icon={
                    <Icon size={"medium"}>
                        <CartIcon/>
                    </Icon>}>
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
