using Anet.Entity;

namespace Bookist.Entities;

public class Link : Entity
{
    public long BookId { get; set; }

    /// <summary>
    /// 下载链接
    /// </summary>
    [Varchar(255)]
    public string Url { get; set; }

    /// <summary>
    /// 访问代码
    /// </summary>
    [Varchar(10)]
    public string Code { get; set; }

    /// <summary>
    /// 文件格式
    /// </summary>
    [Varchar(10)]
    public string Format { get; set; }

    public Book Book { get; set; }
}
