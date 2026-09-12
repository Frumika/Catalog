export const formatDays = (days: number) => {
    const absDays = Math.abs(days);
    const mod10 = absDays % 10;

    let word: string = "дней";

    if (mod10 == 1) {
        word = "день";
    } else if (mod10 >= 2 && mod10 <= 4) {
        word = "дня";
    }

    return `${days} ${word}`;
}