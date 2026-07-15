import styles from "./CartPage.module.css";
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {ContentContainer} from "@/shared/ui/content-container";
import {CartList} from "@/widgets/cart-list";
import {useExtendedCartPositions, useTotalQuantity} from "@/entities/cart";
import {CartSummary} from "@/widgets/cart-summary";
import {useIsAuthenticated} from "@/entities/session";
import {PageLabel} from "@/shared/ui/page-label";


export const CartPage = () => {

    const isAuthenticated = useIsAuthenticated();
    const totalQuantity = useTotalQuantity();
    const {cartPositions} = useExtendedCartPositions(isAuthenticated);

    return (
        <>
            <Header/>

            <main className={styles.main}>
                <ContentContainer>

                    <PageLabel className={styles.pageLabel} title={"Корзина"} quantity={totalQuantity}/>

                    <div className={styles.sectionSpacer}>
                        <CartList cartPositions={cartPositions}/>
                        <CartSummary cartPositions={cartPositions} totalQuantity={totalQuantity}/>
                    </div>

                </ContentContainer>
            </main>

            <Footer/>
        </>
    );
};