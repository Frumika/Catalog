import {useProductList} from "@/entities/product/model/useProductList.ts";
import {Observer} from "@/shared/ui/observer";
import {ProductCard} from "@/entities/product";

import styles from "./ProductList.module.css";
import {ContentContainer} from "@/shared/ui/content-container";


export const ProductList = (
    {}
) => {
    const {
        products,
        hasMore,
        loadMore,
    } = useProductList();

    return (
        <main className={styles.main}>
            <ContentContainer>
                <div className={styles.grid}>
                    {products.map(
                        product => (
                            <ProductCard
                                key={product.productId}
                                product={product}
                                hasButton={true}/>
                        ))}
                </div>

                {hasMore && <Observer onIntersect={loadMore}/>}
            </ContentContainer>
        </main>
    );
}