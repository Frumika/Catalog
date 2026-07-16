import styles from "./WishlistPage.module.css";
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {ContentContainer} from "@/shared/ui/content-container";
import {InfiniteScroll} from "@/shared/ui/infinite-scroll";
import {ProductGrid} from "@/widgets/product-grid";
import {type ProductFilters, useProducts} from "@/entities/product";
import {PageLabel} from "@/shared/ui/page-label";
import {useWishlistTotalQuantity} from "@/entities/wishlist";


export const WishlistPage = () => {

    const filters: ProductFilters = {isWishlist: true,}
    const {items, hasMore, loadMore} = useProducts(filters);
    const totalQuantity = useWishlistTotalQuantity();

    return (
        <>
            <Header/>

            <main className={styles.main}>
                <ContentContainer>
                    <PageLabel className={styles.pageLabel} title={"Избранное"} quantity={totalQuantity}/>

                    <InfiniteScroll hasMore={hasMore} onLoadMore={loadMore}>
                        <ProductGrid products={items}/>
                    </InfiniteScroll>
                </ContentContainer>
            </main>

            <Footer/>
        </>
    );
}