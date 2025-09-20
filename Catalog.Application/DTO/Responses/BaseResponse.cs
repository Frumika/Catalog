namespace Catalog.Application.DTO.Responses;

public abstract class BaseResponse<TCode>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public required TCode Code { get; set; } 
}