namespace FurnitureShop.Common.Models;

public class PaginationMetaData
{
    public PaginationMetaData(int totalCount, int pageSize, int pageNumber)
    {
        CurrentPage = pageNumber;//joriy page raqami
        PageSize = pageSize;//total page count
        TotalCount = totalCount;//umumiy elementlar soni
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    public int CurrentPage { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}