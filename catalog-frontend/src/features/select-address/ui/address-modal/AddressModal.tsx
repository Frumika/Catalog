import type {AddressModalProps} from "./AddressModal.types.ts";
import {Modal} from "@/shared/ui/modal";
import {CloseButton} from "../close-button/CloseButton.tsx";
import {AddressItem} from "../address-item/AddressItem.tsx";
import {Button} from "@/shared/ui/button";
import styles from "./AddressModal.module.css";


export const AddressModal = (
    {
        addresses,
        selectedAddress,
        isOpen,
        onClose,
        onSelect,
        onRemove
    }: AddressModalProps
) => {

    return (
        <Modal isOpen={isOpen} onClose={onClose} className={styles.addressModal}>
            <div className={styles.header}>
                <h2 className={styles.title}>Выберите адрес доставки</h2>
                <CloseButton className={styles.closeButton} onClick={onClose}/>
            </div>

            <div className={styles.main}>
                {addresses?.map(
                    deliveryAddress => (
                        <AddressItem
                            key={deliveryAddress.id}
                            deliveryAddress={deliveryAddress}
                            selected={selectedAddress?.id === deliveryAddress.id}
                            onSelect={onSelect}
                        />
                    ))}
            </div>

            <div className={styles.footer}>
                <Button
                    className={styles.addButton}
                    variant="secondary"
                    size="large"
                    fullWidth>
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