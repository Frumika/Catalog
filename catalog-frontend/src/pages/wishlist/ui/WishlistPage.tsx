import styles from "./WishlistPage.module.css";
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {ContentContainer} from "@/shared/ui/content-container";
import {InfiniteScroll} from "@/shared/ui/infinite-scroll";
import {ProductGrid} from "@/widgets/product-grid";
import {type ProductFilters, useProducts} from "@/entities/product";
import {PageLabel} from "@/shared/ui/page-label";
import {useWishlistTotalQuantity} from "@/entities/wishlist";
import {useState} from "react";
import {SortDropdown} from "@/features/sort-dropdown";


export const WishlistPage = () => {

    const [filters, setFilters] = useState<ProductFilters>({isWishlist: true, sortOrder: 1});
    const {items, hasMore, loadMore} = useProducts(filters);
    const totalQuantity = useWishlistTotalQuantity();

    return (
        <>
            <Header/>

            <main className={styles.main}>
                <ContentContainer>
                    <PageLabel className={styles.pageLabel} title={"Избранное"} quantity={totalQuantity}/>

                    <SortDropdown
                        currentSortOptionId={filters.sortOrder}
                        onSelect={(sortOrder) => setFilters(f => ({...f, sortOrder}))}
                    />

                    <InfiniteScroll hasMore={hasMore} onLoadMore={loadMore}>
                        <ProductGrid products={items}/>
                    </InfiniteScroll>
                </ContentContainer>
            </main>

            <Footer/>
        </>
    );
}