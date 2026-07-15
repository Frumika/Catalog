import styles from "./CartPage.module.css";
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {ContentContainer} from "@/shared/ui/content-container";
import {CartList} from "@/widgets/cart-list";
import {useTotalQuantity} from "@/entities/cart";
import {CartSummary} from "@/widgets/cart-summary";


export const CartPage = () => {

    const totalQuantity = useTotalQuantity();

    return (
        <>
            <Header/>

            <main className={styles.main}>
                <ContentContainer>
                    <div className={styles.listHeader}>
                        <h2 className={styles.title}>
                            Корзина <span className={styles.count}>{totalQuantity}</span>
                        </h2>
                    </div>

                    <div className={styles.sectionSpacer}>
                        <CartList/>
                        <CartSummary/>
                    </div>

                </ContentContainer>
            </main>

            <Footer/>
        </>
    );
};