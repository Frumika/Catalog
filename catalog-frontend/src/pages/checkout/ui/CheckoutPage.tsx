import styles from "./CheckoutPage.module.css";
import {type ExtendedOrder, useGetCheckoutOrderId, useOrderActions} from "@/entities/order";
import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {Footer} from "@/widgets/footer";
import {CheckoutList} from "@/widgets/checkout-list";
import {ContentContainer} from "@/shared/ui/content-container";
import {CheckoutSummary} from "@/widgets/checkout-summary";


export const CheckoutPage = () => {
    const activeOrderId = useGetCheckoutOrderId();
    const [order, setOrder] = useState<ExtendedOrder | null>(null);
    const [error, setError] = useState<ApiError | null>(null);
    const {getOrderById, payOrder, cancelOrder} = useOrderActions();

    useEffect(() => {
        if (!activeOrderId) {
            return;
        }

        getOrderById(activeOrderId)
            .then(order => setOrder(order))
            .catch((error) => setError(toApiError(error)));

    }, [activeOrderId]);

    return (
        <>

            <main className={styles.main}>
                <ContentContainer>
                    {order !== null &&
                        <div className={styles.sectionSpacer}>
                            <CheckoutList order={order}/>
                            <CheckoutSummary orderPositions={order.orderPositions}/>
                        </div>
                    }
                </ContentContainer>
            </main>

            <Footer/>
        </>
    )
}