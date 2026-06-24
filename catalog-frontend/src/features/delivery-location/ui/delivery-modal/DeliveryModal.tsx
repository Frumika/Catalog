import type {DeliveryModalProps} from "./DeliveryModal.types.ts";
import {Modal} from "@/shared/ui/modal";
import styles from "./DeliveryModal.module.css";
import {CloseButton} from "@/features/delivery-location/ui/close-button/CloseButton.tsx";


export const DeliveryModal = (
    {
        isOpen,
        onClose,
    }: DeliveryModalProps
) => {

    // const deliveryModalStyles = [
    //     styles.deliveryModal
    // ].filter(Boolean).join(' ');

    return (
        <Modal isOpen={isOpen} onClose={onClose}>
            <div className={styles.deliveryModal}>
                <div className={styles.header}>
                    <h2 className={styles.title}>Выберите адрес доставки</h2>
                    <CloseButton onClick={onClose}/>
                </div>
            </div>
        </Modal>
    );
}