import type {AddressModalProps} from "./DeliveryModal.types.ts";
import {Modal} from "@/shared/ui/modal";
import {CloseButton} from "../close-button/CloseButton.tsx";
import {AddressItem} from "../address-item/AddressItem.tsx";
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
                <CloseButton className={styles.closeButton} onClick={onClose}/>
            </div>

            <div className={styles.main}>
                <AddressItem
                    id={"318-645"}
                    selected={true}
                    address={"Брянск, Красноармейская ул., 81"}
                    shelfLife={14}
                />
                <AddressItem
                    id={"140-25-48"}
                    address={"Брянск, ул. Костычева, 23к1"}
                    shelfLife={14}
                />
                <AddressItem
                    id={"397-909"}
                    address={"Москва, 11-я Парковая ул., 8"}
                    shelfLife={14}
                />
                <AddressItem
                    id={"948-43"}
                    address={"Брянск, Красноармейская ул., 41"}
                    shelfLife={14}
                />
                <AddressItem
                    id={"104-252"}
                    address={"Санкт-Петербург, ул. Салова, 61"}
                    shelfLife={14}
                />
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