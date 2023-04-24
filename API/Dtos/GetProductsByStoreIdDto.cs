namespace API.Dtos;

public class GetProductsByStoreIdDto
{
    private int _pageSize = 10;
    private const int MaxPageSize = 50;
    private int _pageIndex = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = (value <= 0) ? 1 : value;
    }
}
