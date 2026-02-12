namespace Backend.Domain.Models;

public class Review
{
    public int Id { get; set; }
    public int Score { get; set; }
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }
    public int ProductId { get; set; }

    public User User { get; set; } = null!;
    public Product Product { get; set; } = null!;
}