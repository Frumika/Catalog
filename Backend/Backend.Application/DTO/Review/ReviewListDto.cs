namespace Backend.Application.DTO.Review;

public class ReviewListDto
{
    public List<ReviewDto> Reviews { get; set; } = new();
    public int TotalCount { get; set; }
}