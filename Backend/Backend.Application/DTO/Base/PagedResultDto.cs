namespace Backend.Application.DTO.Base;

public class PagedResultDto<T>
{
    public List<T> Items { get; set; } = null!;
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}