namespace Backend.Application.Common;

public static class PriceCalculator
{
    public static decimal CalculateDiscountPrice(decimal originalPrice, byte discountPercent)
    {
        if (discountPercent == 0) return originalPrice;

        decimal discountedPrice = originalPrice * (100 - discountPercent) / 100m;

        return Math.Round(discountedPrice, 0, MidpointRounding.AwayFromZero);
    }
    
    public static decimal CalculatePositionPrice(decimal originalPrice, byte discountPercent, int quantity)
    {
        decimal pricePerItem = CalculateDiscountPrice(originalPrice, discountPercent);
        return pricePerItem * quantity;
    }
}