namespace Bookist.Web.ViewModels;

public class PagerVM
{
    /// <summary>
    /// 当前页码
    /// </summary>
    public int PageNo { get; set; } = 1;

    /// <summary>
    /// 分页大小
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 总记录数
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
}
