import styles from "./CartPage.module.css";
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {ContentContainer} from "@/shared/ui/content-container";
import {CartList} from "@/widgets/cart-list";
import {useExtendedCartPositions, useCartTotalQuantity} from "@/entities/cart";
import {CartSummary} from "@/widgets/cart-summary";
import {useIsAuthenticated} from "@/entities/session";
import {PageLabel} from "@/shared/ui/page-label";
import {CartSelectionProvider} from "@/features/cart-selection";
import {useOrderActions, useSetActiveCheckoutOrder} from "@/entities/order";
import {useCurrentPickupPoint} from "@/entities/pickup-point";
import {useNavigate} from "react-router-dom";


export const CartPage = () => {
    const isAuthenticated = useIsAuthenticated();
    const totalQuantity = useCartTotalQuantity();
    const {cartPositions} = useExtendedCartPositions(isAuthenticated);

    const {makeOrder} = useOrderActions();
    const setActiveOrder = useSetActiveCheckoutOrder();
    const pickupPoint = useCurrentPickupPoint();
    const navigate = useNavigate();

    const handleCheckout = async (productIds: number[]) => {
        if (!pickupPoint) return;

        const createdOrder = await makeOrder(productIds, pickupPoint.id);
        setActiveOrder(createdOrder);

        navigate('/checkout');
    }

    return (
        <>
            <Header/>

            <main className={styles.main}>
                <ContentContainer>

                    <PageLabel className={styles.pageLabel} title={"Корзина"} quantity={totalQuantity}/>

                    <CartSelectionProvider cartPositions={cartPositions}>
                        <div className={styles.sectionSpacer}>
                            <CartList cartPositions={cartPositions} onCheckout={handleCheckout}/>
                            <CartSummary onCheckout={handleCheckout}/>
                        </div>
                    </CartSelectionProvider>

                </ContentContainer>
            </main>

            <Footer/>
        </>
    );
};