namespace Backend.Application.DTO.Requests;


public class GetProductsRequest
{
    public int? CategoryId { get; set; }    
    public int Page { get; set; }
    public int PageSize { get; set; }
}