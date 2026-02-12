namespace Backend.Application.DTO.Requests.Review;

public class UpdateRequest
{
    public string UserSessionId { get; set; } = string.Empty;
    public int ReviewId { get; set; }
    public int Score { get; set; }
    public string? Text { get; set; }
}