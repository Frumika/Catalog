import {useProductList} from "@/entities/product/model/useProductList.ts";
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {ContentContainer} from "@/shared/ui/content-container";
import {CartList} from "@/features/cart-list";
import {ProductList} from "@/features/product-list";
import styles from "./CartPage.module.css";
import {useCartPage} from "@/entities/cart";
import {useIsAuthenticated} from "@/entities/session";


export const CartPage = () => {

    const isAuthenticated = useIsAuthenticated();
    const {cartPositions} = useCartPage(isAuthenticated);

    return (
        <div className={styles.pageWrapper}>
            <Header/>

            <main className={styles.main}>
                <ContentContainer>
                    <div className={styles.sectionSpacer}>
                        <CartList cartPositions={cartPositions} />
                    </div>

                </ContentContainer>
            </main>

            <Footer/>
        </div>
    );
};