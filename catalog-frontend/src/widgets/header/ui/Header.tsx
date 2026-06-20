import {useState} from "react";
import {Logo} from "@/shared/ui/logo";
import {Button} from "@/shared/ui/button";
import {SearchBar} from "@/features/search/search-bar";
import {NavButton} from "@/widgets/header/ui/nav-button";
import {CategoryButton} from "@/widgets/header/ui/category-button";
import {ContentContainer} from "@/shared/ui/content-container";

import CatalogIcon from "@/shared/assets/catalog.svg?react";
import ProfileIcon from "@/shared/assets/profile.svg?react";
import OrderIcon from "@/shared/assets/order.svg?react";
import WishIcon from "@/shared/assets/wish.svg?react";
import CartIcon from "@/shared/assets/cart.svg?react";

import styles from "./Header.module.css"


export const Header = (
    {}
) => {
    const [query, setQuery] = useState('');

    return (
        <header className={styles.header}>
            <ContentContainer>
                <div className={styles.content}>
                    <div className={styles.upper}>
                        <Logo/>

                        <Button
                            variant="primary"
                            size="medium"
                            icon={<CatalogIcon/>}>
                            Каталог
                        </Button>

                        <SearchBar
                            className={styles.searchBar}
                            value={query}
                            placeholder={"Ищите на Wildboars"}
                            onChange={setQuery}
                            onSearch={() => console.log(query)}
                            onClear={() => setQuery("")}
                        />

                        <div className={styles.navItemContainer}>

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
                        </div>
                    </div>

                    <div className={styles.bottom}>
                        <CategoryButton
                            onClick={() => {
                            }}>
                            Одежда
                        </CategoryButton>

                        <CategoryButton
                            onClick={() => {
                            }}>
                            Электроника
                        </CategoryButton>

                        <CategoryButton
                            onClick={() => {
                            }}>
                            Дом и сад
                        </CategoryButton>

                        <CategoryButton
                            onClick={() => {
                            }}>
                            Сертификаты
                        </CategoryButton>
                    </div>
                </div>
            </ContentContainer>
        </header>
    );

}