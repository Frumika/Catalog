import {useState} from "react";
import {Logo} from "@/shared/ui/logo";
import {CatalogButton} from "./catalog-button/CatalogButton.tsx";
import {SearchBar} from "@/features/search-bar";
import {CategoryButton} from "./category-button/CategoryButton.tsx";
import {ContentContainer} from "@/shared/ui/content-container";
import {SelectPickupPoint} from "@/features/select-pickup-point/ui/SelectPickupPoint.tsx";
import {NavGroup} from "./nav-group/NavGroup.tsx";
import {type ComponentDisplayMode, useMediaQuery} from "@/shared/lib";
import styles from "./Header.module.css"


export const Header = (
    {}
) => {
    const [query, setQuery] = useState('');

    const isLaptop = useMediaQuery('(max-width: 1200px)');
    const isTablet = useMediaQuery('(max-width: 1100px)');
    const isMobile = useMediaQuery('(max-width: 950px)');

    const logoDisplayMode: ComponentDisplayMode = isTablet ? 'compact' : 'full';
    const catalogDisplayMode: ComponentDisplayMode = isLaptop ? 'compact' : 'full';
    const navDisplayMode: ComponentDisplayMode = isMobile ? 'compact' : 'full';

    return (
        <header className={styles.header}>
            <ContentContainer>
                <div className={styles.content}>
                    <div className={styles.upper}>
                        <Logo displayMode={logoDisplayMode}/>

                        <CatalogButton displayMode={catalogDisplayMode}/>

                        <SearchBar
                            className={styles.searchBar}
                            value={query}
                            placeholder={"Ищите на Wildboars"}
                            onChange={setQuery}
                            onSearch={() => console.log(query)}
                            onClear={() => setQuery("")}
                        />

                        <NavGroup displayMode={navDisplayMode}/>
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

                        <SelectPickupPoint/>
                    </div>
                </div>
            </ContentContainer>
        </header>
    );
}