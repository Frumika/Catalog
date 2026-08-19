import styles from "./CheckoutHeader.module.css";
import {ContentContainer} from "@/shared/ui/content-container";
import {Logo} from "@/shared/ui/logo";
import RefundIcon from "@/shared/assets/icons/refund.svg?react";
import DeliveryIcon from "@/shared/assets/icons/delivery.svg?react";
import {Icon} from "@/shared/ui/icon";


export const CheckoutHeader = () => {

    return (

        <header className={styles.checkoutHeader}>
            <ContentContainer>
                <div className={styles.content}>

                    <Logo/>

                    <div className={styles.infoContainer}>
                        <div className={styles.info}>
                            <Icon size={"large"}><RefundIcon/></Icon>

                            <div className={styles.textContainer}>
                                <span className={styles.boldText}>
                                    Гарантия легкого возврата
                                </span>
                                <span className={styles.normalText}>
                                    Заберем товар и быстро вернем деньги
                                </span>
                            </div>
                        </div>

                        <div className={styles.info}>
                            <Icon size={"large"}><DeliveryIcon/></Icon>

                            <div className={styles.textContainer}>
                                <span className={styles.boldText}>
                                    Доставка курьером без доплат от 2 500 ₽
                                </span>
                                <span className={styles.normalText}>
                                    Доставка заказов до 2 500 ₽ – 149 ₽ / 249 ₽
                                </span>
                            </div>
                        </div>
                    </div>

                </div>
            </ContentContainer>
        </header>

    )
        ;
}