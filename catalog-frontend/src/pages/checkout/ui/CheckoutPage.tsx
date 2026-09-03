import styles from "./CheckoutPage.module.css";
import {type ExtendedOrder, type OrderPositionGroup, useGetCheckoutOrderId, useOrderActions} from "@/entities/order";
import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {Footer} from "@/widgets/footer";
import {CheckoutList} from "@/widgets/checkout-list";
import {ContentContainer} from "@/shared/ui/content-container";
import {CheckoutSummary} from "@/widgets/checkout-summary";
import {CheckoutHeader} from "./checkout-header/CheckoutHeader.tsx";
import {DeliveryType} from "./delivery-type/DeliveryType.tsx";
import {PageLabel} from "@/shared/ui/page-label";
import {CheckoutModal} from "@/widgets/checkout-modal";
import {useDisclosure} from "@/shared/lib";


export const CheckoutPage = () => {
    const activeOrderId = useGetCheckoutOrderId();
    const [order, setOrder] = useState<ExtendedOrder | null>(null);
    const [error, setError] = useState<ApiError | null>(null);
    const {getOrderById, payOrder} = useOrderActions();
    const [selectedGroup, setSelectedGroup] = useState<OrderPositionGroup | null>(null);
    const {isOpen, open, close} = useDisclosure();


    useEffect(() => {
        if (!activeOrderId) {
            return;
        }

        getOrderById(activeOrderId)
            .then(order => setOrder(order))
            .catch((error) => setError(toApiError(error)));

    }, [activeOrderId]);

    const onModalClose = () => {
        close();
        setSelectedGroup(null);
    }

    const onModalOpen = (positionGroup: OrderPositionGroup) => {
        open();
        setSelectedGroup(positionGroup);
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
                                onPay={() => payOrder(order.orderId)}
                            />
                        </div>
                    }
                </ContentContainer>
            </main>

            <Footer/>
        </>
    )
}