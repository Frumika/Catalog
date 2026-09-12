export const formatGoodsQuantity = (quantity: number) => {
    const absQuantity = Math.abs(quantity);
    const mod100 = absQuantity % 100;
    const mod10 = absQuantity % 10;

    let word = 'товаров';

    if (mod100 < 11 || mod100 > 19) {
        if (mod10 === 1) {
            word = 'товар';
        } else if (mod10 >= 2 && mod10 <= 4) {
            word = 'товара';
        }
    }

    return `${quantity} ${word}`;
}