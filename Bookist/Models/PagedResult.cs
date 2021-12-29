namespace Bookist.Models;

public class PagedResult<T> where T : new()
{
    public PagedResult() { }

    public PagedResult(int page, int size)
    {
        Size = size;
        Page = page;
    }

    public int Page { get; }
    public int Size { get; }
    public int Total { get; set; }
    public IEnumerable<T> List { get; set; } = Enumerable.Empty<T>();
}
