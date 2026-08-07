import styles from "./CartPage.module.css";
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {ContentContainer} from "@/shared/ui/content-container";
import {CartList} from "@/widgets/cart-list";
import {useExtendedCartPositions, useCartTotalQuantity} from "@/entities/cart";
import {CartSummary} from "@/widgets/cart-summary";
import {useIsAuthenticated} from "@/entities/session";
import {PageLabel} from "@/shared/ui/page-label";
import {useCartSelection} from "@/pages/cart/model/useCartSelection.ts";


export const CartPage = () => {

    const isAuthenticated = useIsAuthenticated();
    const totalQuantity = useCartTotalQuantity();
    const {cartPositions} = useExtendedCartPositions(isAuthenticated);
    const {
        selectedPositions,
        isPositionsSelected,
        togglePosition
    } = useCartSelection(cartPositions);

    return (
        <>
            <Header/>

            <main className={styles.main}>
                <ContentContainer>

                    <PageLabel className={styles.pageLabel} title={"Корзина"} quantity={totalQuantity}/>

                    <div className={styles.sectionSpacer}>
                        <CartList
                            cartPositions={cartPositions}
                            isSelected={isPositionsSelected}
                            onTogglePosition={togglePosition}
                        />
                        <CartSummary cartPositions={selectedPositions} totalQuantity={totalQuantity}/>
                    </div>

                </ContentContainer>
            </main>

            <Footer/>
        </>
    );
};