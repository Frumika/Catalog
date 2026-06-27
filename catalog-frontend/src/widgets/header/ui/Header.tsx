import {useState} from "react";
import {Logo} from "@/shared/ui/logo";
import {CatalogButton} from "@/widgets/header/ui/catalog-button";
import {SearchBar} from "@/features/search-bar";
import {NavButton} from "@/widgets/header/ui/nav-button";
import {CategoryButton} from "@/widgets/header/ui/category-button";
import {ContentContainer} from "@/shared/ui/content-container";
import {SelectAddress} from "@/features/select-address/ui/SelectAddress.tsx";

import ProfileIcon from "@/shared/assets/icons/profile.svg?react";
import OrderIcon from "@/shared/assets/icons/order.svg?react";
import WishIcon from "@/shared/assets/icons/wish.svg?react";
import CartIcon from "@/shared/assets/icons/cart.svg?react";

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
                        <Logo hideText={false}/>

                        <CatalogButton hideText={false}/>

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
                                icon={<ProfileIcon/>}
                                hideText={false}>
                                Войти
                            </NavButton>

                            <NavButton
                                icon={<OrderIcon/>}
                                badgeValue={150}
                                hideText={false}>
                                Заказы
                            </NavButton>

                            <NavButton
                                icon={<WishIcon/>}
                                badgeValue={5}
                                hideText={false}>
                                Избранное
                            </NavButton>

                            <NavButton
                                icon={<CartIcon/>}
                                badgeValue={10}
                                hideText={false}>
                                Корзина
                            </NavButton>
                        </div>
                    </div>

                    <div className={styles.bottom}>
                        <div className={styles.categoryItemContainer}>
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

                        <SelectAddress/>
                    </div>
                </div>
            </ContentContainer>
        </header>
    );
}