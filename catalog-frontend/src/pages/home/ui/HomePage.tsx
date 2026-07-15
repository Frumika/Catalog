import styles from "./HomePage.module.css"
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {useProductList} from "@/entities/product/model/useProductList.ts";
import {ContentContainer} from "@/shared/ui/content-container";
import {ProductGrid} from "@/widgets/product-grid";
import {InfiniteScroll} from "@/shared/ui/infinite-scroll";


export const HomePage = () => {
    const {items, hasMore, loadMore} = useProductList();

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