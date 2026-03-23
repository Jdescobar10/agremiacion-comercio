namespace GesComercio.Application.DTOs;

public class PagedResponseDto<T>
{
    public IEnumerable<T> Data { get; set; } = new List<T>();
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}