import {useEffect, useRef, useState} from "react";
import {Logo} from "@/shared/ui/logo";
import {CatalogButton} from "./catalog-button/CatalogButton.tsx";
import {SearchBar} from "@/features/search-bar";
import {CategoryButton} from "./category-button/CategoryButton.tsx";
import {ContentContainer} from "@/shared/ui/content-container";
import {SelectPickupPoint} from "@/features/select-pickup-point/ui/SelectPickupPoint.tsx";
import {NavGroup} from "./nav-group/NavGroup.tsx";
import {type ComponentDisplayMode, useMediaQuery, useNotify} from "@/shared/lib";
import styles from "./Header.module.css"


interface HeaderProps {
    disabledLogo?: boolean;
}

export const Header = (
    {
        disabledLogo = false,
    }: HeaderProps
) => {
    const [query, setQuery] = useState('');
    const [isSticky, setIsSticky] = useState(false);
    const [upperHeight, setUpperHeight] = useState(0);

    const sentinelRef = useRef<HTMLDivElement>(null);
    const upperRef = useRef<HTMLDivElement>(null);

    const isLaptop = useMediaQuery('(max-width: 1200px)');
    const isTablet = useMediaQuery('(max-width: 1100px)');
    const isMobile = useMediaQuery('(max-width: 950px)');

    const logoDisplayMode: ComponentDisplayMode = isTablet ? 'compact' : 'full';
    const catalogDisplayMode: ComponentDisplayMode = isLaptop ? 'compact' : 'full';
    const navDisplayMode: ComponentDisplayMode = isMobile ? 'compact' : 'full';

    const notify = useNotify();
    const onCategoryClick = () => {
        notify("warning", "Кнопки категорий пока не реализованы");
    }

    useEffect(() => {
        const upperEl = upperRef.current;
        if (!upperEl) return;

        const resizeObserver = new ResizeObserver(([entry]) => {
            setUpperHeight(entry.borderBoxSize[0].blockSize);
        });

        resizeObserver.observe(upperEl);

        const observer = new IntersectionObserver(
            ([entry]) => {
                setIsSticky(!entry.isIntersecting);
            },
            {threshold: 1.0}
        );

        if (sentinelRef.current) {
            observer.observe(sentinelRef.current);
        }

        return () => {
            resizeObserver.disconnect();
            observer.disconnect();
        };
    }, []);


    const upperStyles = [
        styles.upper,
        isSticky && styles.upperSticky
    ].filter(Boolean).join(' ');

    return (
        <>
            <div ref={sentinelRef} className={styles.sentinel}/>

            <header className={styles.upperWrapper}>
                <ContentContainer>
                    <div ref={upperRef} className={upperStyles}>

                        <Logo disabled={disabledLogo} displayMode={logoDisplayMode}/>
                        <CatalogButton displayMode={catalogDisplayMode}/>

                        <SearchBar
                            className={styles.searchBar}
                            value={query}
                            placeholder={"Ищите на Wildboars"}
                            onChange={setQuery}
                            onSearch={() => notify("warning", "Поиск товара пока не реализован")}
                            onClear={() => setQuery("")}
                        />

                        <NavGroup displayMode={navDisplayMode}/>
                    </div>
                </ContentContainer>
            </header>

            <div className={styles.bottomWrapper}>

                <ContentContainer>
                    <div className={styles.bottom} style={{paddingTop: `${upperHeight}px`}}>

                        <div className={styles.categoryItemContainer}>
                            <CategoryButton onClick={onCategoryClick}>
                                Одежда
                            </CategoryButton>

                            <CategoryButton onClick={onCategoryClick}>
                                Электроника
                            </CategoryButton>

                            <CategoryButton onClick={onCategoryClick}>
                                Дом и сад
                            </CategoryButton>

                            <CategoryButton onClick={onCategoryClick}>
                                Сертификаты
                            </CategoryButton>
                        </div>
                        <SelectPickupPoint/>
                    </div>
                </ContentContainer>
            </div>
        </>
    );
};