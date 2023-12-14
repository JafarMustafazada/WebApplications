namespace MVC_PustokPlus.Helpers;

public class PaginationPager
{
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int EndPage { get; set; }


    public PaginationPager(int count, int currentPage, int pageSize)
    {
        int itemsLeft = count - (currentPage * pageSize);

        this.CurrentPage = currentPage;
        this.PageSize = pageSize;

    }
}
