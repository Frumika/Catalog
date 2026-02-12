namespace Backend.Application.DTO.Entities.Review;

public class ReviewDto
{
    public int Id { get; set; }
    public int Score { get; set; }
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}