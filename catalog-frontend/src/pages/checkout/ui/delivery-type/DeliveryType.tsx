import styles from "./DeliveryType.module.css";
import {type PickupPoint, useCurrentPickupPoint} from "@/entities/pickup-point";
import type {User} from "@/entities/user";
import {useIsAuthenticated} from "@/entities/session";
import {useUser} from "@/entities/user/model/useUser.ts";


export const DeliveryType = () => {
    const isAuthenticated = useIsAuthenticated();
    const pickupPoint = useCurrentPickupPoint();
    const {user} = useUser(isAuthenticated);


    return (

        <div className={styles.deliveryType}>
            <h2 className={styles.header}>Доставка Wildboars</h2>

            <div className={styles.pickupPoint}>
                <p className={styles.pointLabel}>Пункт Wildboars</p>
                <p className={styles.address}> {pickupPoint.address} </p>
                <p className={styles.shelfLifetime}>{pickupPoint.shelfLifetime}</p>
            </div>

            <div className={styles.user}>
                <p className={styles.login}>{user.login}</p>
                <p className={styles.phoneNumber}>{user.phoneNumber}</p>
            </div>
        </div>
        
    );
}