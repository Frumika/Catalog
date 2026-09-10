export const formatPhone = (phone: string) => {

    let numbers: number[] = [];
    for (let i = 0; i < phone.length; i++) {
        let char = phone.charAt(i);
        let number = Number(char);

        if (number >= 0 && number <= 9) {
            numbers.push(number);
        }
    }

    let result: string = '';
    let part = "";

    for (let i = 0; i < numbers.length; i++) {
        if (i == 1) {
            result += `+${part} `;
            part = '';
        } else if (i == 4) {
            result += `(${part}) `;
            part = '';
        } else if (i == 7 || i == 9) {
            result += `${part}-`;
            part = '';
        }

        part += numbers[i];
    }

    result += part;

    return result;
}