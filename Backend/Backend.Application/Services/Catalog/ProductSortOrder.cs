namespace Backend.Application.Services.Catalog;

public enum ProductSortOrder
{
    Default = 0,         // Например, по ID или по популярности
    Newest = 1,          // Сначала новые (по дате добавления товара)
    Oldest = 2,          // Сначала старые
    PriceAsc = 3,        // Сначала дешевые
    PriceDesc = 4,       // Сначала дорогие
    DiscountDesc = 5     // По величине скидки (от большей к меньшей)
}