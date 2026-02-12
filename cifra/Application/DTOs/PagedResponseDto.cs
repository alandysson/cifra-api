namespace controleDeGastos.Application.DTOs;

public record PaginationDto(int Page, int PageSize, int TotalItems, int TotalPages);

public record PagedResponseDto<T>(IEnumerable<T> Data, PaginationDto Pagination)
{
    public static PagedResponseDto<T> Create(IEnumerable<T> source, int page, int pageSize)
    {
        var totalItems = source.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        var data = source.Skip((page - 1) * pageSize).Take(pageSize);

        return new PagedResponseDto<T>(data, new PaginationDto(page, pageSize, totalItems, totalPages));
    }
}
