import {useProductList} from "@/entities/product/model/useProductList.ts";
import {Header} from "@/widgets/header";
import {ContentContainer} from "@/shared/ui/content-container";
import {ProductList} from "@/features/product-list";
import styles from "./CartPage.module.css"
import {Footer} from "@/widgets/footer";


export const CartPage = () => {
    const {items, hasMore, loadMore} = useProductList();

    return (
        <>
            <Header/>

            <main className={styles.main}>
                <ContentContainer>
                    <h1>Корзина</h1>
                    <ProductList
                        hasMore={hasMore}
                        products={items}
                        onLoadMore={loadMore}
                    />
                    <h1>Рекомендуем</h1>
                    <ProductList
                        hasMore={hasMore}
                        products={items}
                        onLoadMore={loadMore}
                    />
                </ContentContainer>
            </main>

            <Footer/>
        </>
    );
};