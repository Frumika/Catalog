import styles from "./HomePage.module.css"
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {ContentContainer} from "@/shared/ui/content-container";
import {ProductGrid} from "@/widgets/product-grid";
import {InfiniteScroll} from "@/shared/ui/infinite-scroll";
import {useProducts} from "@/entities/product";


export const HomePage = () => {
    const {items, hasMore, loadMore} = useProducts();

    return (
        <>
            <Header disabledLogo/>

            <main className={styles.main}>
                <ContentContainer>
                    <InfiniteScroll hasMore={hasMore} onLoadMore={loadMore}>
                        <ProductGrid products={items}/>
                    </InfiniteScroll>
                </ContentContainer>
            </main>

            <Footer/>
        </>
    );
};