import styles from "./CheckoutModal.module.css";
import {Modal} from "@/shared/ui/modal";
import {CheckoutPositionCard, type OrderPositionGroup} from "@/entities/order";
import {formatDate, formatGoodsQuantity, getGoodsQuantity} from "@/shared/lib";


interface CheckoutModalProps {
    isOpen: boolean;
    onClose: () => void;
    positionsGroup: OrderPositionGroup;
}

export const CheckoutModal = (
    {
        isOpen,
        onClose,
        positionsGroup,
    }: CheckoutModalProps
) => {

    const goodsQuantity = getGoodsQuantity(positionsGroup.orderPositions);
    const lastPositionId = positionsGroup.orderPositions[positionsGroup.orderPositions.length - 1].productId;


    return (
        <Modal isOpen={isOpen} onClose={onClose} className={styles.checkoutModal}>
            <div className={styles.header}>
                <h2 className={styles.deliveryDate}>
                    Ожидаемая дата доставки: {formatDate(positionsGroup.date)}
                </h2>
                <p className={styles.quantity}>
                    {formatGoodsQuantity(goodsQuantity)}
                </p>
            </div>

            <div className={styles.content}>
                {positionsGroup.orderPositions.map(position =>
                    <CheckoutPositionCard orderPosition={position} isLastCard={position.productId == lastPositionId}/>
                )}
            </div>

        </Modal>
    );
}