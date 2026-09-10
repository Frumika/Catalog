export const formatDate = (date: Date): string => {
    if (isNaN(date.getTime())) {
        return '';
    }

    const formatter = new Intl.DateTimeFormat(
        'ru-RU',
        {
            day: 'numeric',
            month: 'long',
        }
    );

    return formatter.format(date);
}