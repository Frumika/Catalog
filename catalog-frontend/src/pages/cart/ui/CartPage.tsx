// pages/cart/ui/CartPage.tsx
import { useProductList } from "@/entities/product/model/useProductList.ts";
import { Header } from "@/widgets/header";
import { Footer } from "@/widgets/footer";
import { ContentContainer } from "@/shared/ui/content-container";
import { CartList } from "@/features/cart-list";
import { ProductList } from "@/features/product-list";
import styles from "./CartPage.module.css";

export const CartPage = () => {
    // Используем рекомендации (items) из entities/product
    const { items, hasMore, loadMore } = useProductList();

    return (
        <div className={styles.pageWrapper}>
            <Header />

            <main className={styles.main}>
                <ContentContainer>
                    {/* Виджет Корзины Ozon (Список товаров + Сайдбар оплаты) */}
                    <div className={styles.sectionSpacer}>
                        <CartList />
                    </div>

                    {/* Блок Рекомендаций Ozon */}
                    <div className={styles.recommendationsSection}>
                        <h2 className={styles.recommendationsTitle}>Рекомендуем вам</h2>
                        <ProductList
                            hasMore={hasMore}
                            products={items}
                            onLoadMore={loadMore}
                        />
                    </div>
                </ContentContainer>
            </main>

            <Footer />
        </div>
    );
};