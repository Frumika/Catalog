import styles from "./CartPage.module.css";
import {Header} from "@/widgets/header";
import {Footer} from "@/widgets/footer";
import {ContentContainer} from "@/shared/ui/content-container";
import {CartList} from "./cart-list/CartList.tsx";
import {useExtendedCartPositions, useCartTotalQuantity} from "@/entities/cart";
import {CartSummary} from "./cart-summary/CartSummary.tsx";
import {useIsAuthenticated} from "@/entities/session";
import {PageLabel} from "@/shared/ui/page-label";
import {CartSelectionProvider} from "@/features/cart-selection";
import {useOrderActions, useSetActiveCheckoutOrder} from "@/entities/order";
import {useCurrentPickupPoint} from "@/entities/pickup-point";
import {useNavigate} from "react-router-dom";
import {Button} from "@/shared/ui/button";


export const CartPage = () => {
    const isAuthenticated = useIsAuthenticated();
    const totalQuantity = useCartTotalQuantity();
    const {cartPositions} = useExtendedCartPositions(isAuthenticated);
    const {makeOrder} = useOrderActions();
    const setActiveOrder = useSetActiveCheckoutOrder();
    const pickupPoint = useCurrentPickupPoint();
    const navigate = useNavigate();

    const isCartEmpty = totalQuantity === 0
    const displayedLabelText: string = isCartEmpty ? "Корзина пуста" : "Корзина";
    const displayedQuantity: number | undefined = totalQuantity === 0 ? undefined : totalQuantity;


    const handleCheckout = async (productIds: number[]) => {
        if (!pickupPoint) return;

        const createdOrder = await makeOrder(productIds, pickupPoint.id);
        if (!createdOrder) return;

        setActiveOrder(createdOrder);

        navigate('/checkout');
    }

    return (
        <>
            <Header/>

            <main className={styles.main}>
                <ContentContainer>

                    <PageLabel title={displayedLabelText} quantity={displayedQuantity}/>

                    {!isCartEmpty ?
                        <CartSelectionProvider cartPositions={cartPositions}>
                            <div className={styles.sectionSpacer}>
                                <CartList cartPositions={cartPositions} onCheckout={handleCheckout}/>
                                <CartSummary onCheckout={handleCheckout}/>
                            </div>
                        </CartSelectionProvider>
                        :
                        <div className={styles.plugContainer}>
                            <p className={styles.plugText}>
                                Что-бы что-то купить, это надо сначала положить в корзину
                            </p>
                            <Button
                                className={styles.navigateButton}
                                variant={"secondary"}
                                size={"small"}
                                onClick={() => navigate("/")}
                            >
                                Начать покупки
                            </Button>
                        </div>
                    }
                </ContentContainer>
            </main>

            <Footer/>
        </>
    );
};