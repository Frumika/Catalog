import styles from "./CheckoutPage.module.css";
import {type ExtendedOrder, type OrderPositionGroup, useGetCheckoutOrderId, useOrderActions} from "@/entities/order";
import {useEffect, useState} from "react";
import {Footer} from "@/widgets/footer";
import {CheckoutList} from "./checkout-list/CheckoutList.tsx";
import {ContentContainer} from "@/shared/ui/content-container";
import {CheckoutSummary} from "./checkout-summary/CheckoutSummary.tsx";
import {CheckoutHeader} from "./checkout-header/CheckoutHeader.tsx";
import {DeliveryType} from "./delivery-type/DeliveryType.tsx";
import {PageLabel} from "@/shared/ui/page-label";
import {CheckoutModal} from "./checkout-modal/CheckoutModal.tsx";
import {useDisclosure, useNotify} from "@/shared/lib";
import {useNavigate} from "react-router-dom";
import {useCartActions} from "@/entities/cart";


export const CheckoutPage = () => {
    const activeOrderId = useGetCheckoutOrderId();
    const [order, setOrder] = useState<ExtendedOrder | null>(null);
    const {getOrderById, payOrder} = useOrderActions();
    const {clearCart} = useCartActions();
    const [selectedGroup, setSelectedGroup] = useState<OrderPositionGroup | null>(null);
    const {isOpen, open, close} = useDisclosure();
    const navigate = useNavigate();
    const notify = useNotify();


    useEffect(() => {
        if (!activeOrderId) {
            return;
        }

        getOrderById(activeOrderId)
            .then(order => setOrder(order || null));

    }, [activeOrderId]);

    const onModalClose = () => {
        close();
        setSelectedGroup(null);
    }

    const onModalOpen = (positionGroup: OrderPositionGroup) => {
        open();
        setSelectedGroup(positionGroup);
    }

    const onPay = async (orderId: number) => {
        await payOrder(orderId);
        await clearCart();
        notify("success", "Оплата прошла успешно");
        navigate("/");
    }


    return (
        <>
            <CheckoutHeader/>

            <main className={styles.main}>
                <ContentContainer>
                    <PageLabel className={styles.pageLabel} title={"Оформление заказа"}/>


                    {order !== null &&
                        <div className={styles.sectionSpacer}>

                            <div className={styles.leftSection}>
                                <DeliveryType/>
                                <CheckoutList order={order} onGroupSelect={onModalOpen}/>
                            </div>
                            {selectedGroup &&
                                <CheckoutModal
                                    isOpen={isOpen}
                                    onClose={onModalClose}
                                    positionsGroup={selectedGroup}
                                />
                            }

                            <CheckoutSummary
                                orderPositions={order.orderPositions}
                                onPay={() => onPay(order.orderId)}
                            />
                        </div>
                    }
                </ContentContainer>
            </main>

            <Footer/>
        </>
    )
}