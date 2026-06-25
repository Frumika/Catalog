import type {AddressModalProps} from "./DeliveryModal.types.ts";
import {Modal} from "@/shared/ui/modal";
import {CloseButton} from "../close-button/CloseButton.tsx";
import {Button} from "@/shared/ui/button";
import styles from "./AddressModal.module.css";


export const AddressModal = (
    {
        isOpen,
        onClose,
    }: AddressModalProps
) => {

    return (
        <Modal isOpen={isOpen} onClose={onClose} className={styles.addressModal}>
            <div className={styles.header}>
                <h2 className={styles.title}>Выберите адрес доставки</h2>
                <CloseButton onClick={onClose}/>
            </div>

            <div className={styles.main}>
                {/* AddressItem-ы */}
            </div>

            <div className={styles.footer}>
                <Button
                    className={styles.addButton}
                    variant="secondary"
                    size="large"
                    fullWidth>
                    <div className={styles.buttonContent}>
                        <span className={styles.buttonTitle}>Добавить</span>
                        <span className={styles.buttonDescription}>
                        адрес доставки, пункт выдачи, постамат
                    </span>
                    </div>
                </Button>
            </div>
        </Modal>
    );
}