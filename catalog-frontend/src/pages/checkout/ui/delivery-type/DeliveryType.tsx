import styles from "./DeliveryType.module.css";
import {useCurrentPickupPoint} from "@/entities/pickup-point";
import {useIsAuthenticated} from "@/entities/session";
import {useUser} from "@/entities/user/model/useUser.ts";
import {formatDays, formatPhone} from "@/shared/lib";
import PlaceIcon from "@/shared/assets/icons/place.svg?react";
import ProfileFillIcon from "@/shared/assets/icons/profile-fill.svg?react";


export const DeliveryType = () => {
    const isAuthenticated = useIsAuthenticated();
    const pickupPoint = useCurrentPickupPoint();
    const {user} = useUser(isAuthenticated);

    if (!pickupPoint || !user) {
        return null;
    }

    return (
        <div className={styles.deliveryType}>
            <h2 className={styles.header}>Доставка Wildboars</h2>

            <div className={[styles.pickupPoint, styles.hasGap].filter(Boolean).join(' ')}>
                <PlaceIcon className={styles.icon}/>

                <div className={styles.pointDescription}>
                    <p className={styles.pointLabel}>Пункт выдачи заказа</p>

                    <div className={styles.pointInfo}>
                        <p className={styles.address}> {pickupPoint.address} </p>
                        <p className={styles.shelfLifetime}> {`Срок хранения заказа – ${formatDays(pickupPoint.shelfLifetime)}`}</p>
                    </div>
                </div>
            </div>

            <div className={styles.dividingLine}/>

            <div className={[styles.user, styles.hasGap].filter(Boolean).join(' ')}>
                <ProfileFillIcon className={styles.icon}/>
                <p className={styles.credentials}>{`${user.login} ${formatPhone(user.phoneNumber)}`}</p>
            </div>
        </div>
    );
}