import {useProductList} from "@/entities/product/model/useProductList.ts";
import {Header} from "@/widgets/header";
import {ContentContainer} from "@/shared/ui/content-container";
import {ProductList} from "@/features/product-list";
import styles from "./HomePage.module.css"


export const HomePage = () => {
    const {items, hasMore, loadMore} = useProductList();

    return (
        <>
            <Header/>
            <main className={styles.main}>
                <ContentContainer>
                    <ProductList
                        hasMore={hasMore}
                        products={items}
                        onLoadMore={loadMore}
                    />
                </ContentContainer>
            </main>
        </>
    );
};