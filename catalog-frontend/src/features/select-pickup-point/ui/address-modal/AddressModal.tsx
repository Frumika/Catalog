import styles from "./AddressModal.module.css";
import type {AddressModalProps} from "./AddressModal.types.ts";
import {Modal} from "@/shared/ui/modal";
import {AddressCard} from "@/features/select-pickup-point/ui/address-card/AddressCard.tsx";
import {Button} from "@/shared/ui/button";
import {useCurrentPickupPoint, usePickupPointActions, usePickupPoints} from "@/entities/pickup-point";
import {useNotify} from "@/shared/lib";


export const AddressModal = (
    {
        isOpen,
        onClose,
    }: AddressModalProps
) => {

    const pickupPoints = usePickupPoints()
    const currentPickupPoint = useCurrentPickupPoint();
    const {selectPickupPoint} = usePickupPointActions()
    const notify = useNotify();

    return (
        <Modal isOpen={isOpen} onClose={onClose} className={styles.addressModal}>
            <div className={styles.header}>
                <h2 className={styles.title}>Выберите адрес доставки</h2>
            </div>

            <div className={styles.main}>
                {pickupPoints?.map(
                    deliveryAddress => (
                        <AddressCard
                            key={deliveryAddress.id}
                            pickupPoint={deliveryAddress}
                            selected={currentPickupPoint?.id === deliveryAddress.id}
                            onSelect={selectPickupPoint}
                        />
                    ))}
            </div>

            <div className={styles.footer}>
                <Button
                    className={styles.addButton}
                    variant="secondary"
                    size="large"
                    fullWidth
                    onClick={() => notify("warning", "Данный функционал пока не реализован")}>
                    <div className={styles.buttonContent}>
                        <span className={styles.buttonTitle}>
                            Добавить
                        </span>

                        <span className={styles.buttonDescription}>
                            адрес доставки, пункт выдачи, постамат
                        </span>
                    </div>
                </Button>
            </div>
        </Modal>
    );
}