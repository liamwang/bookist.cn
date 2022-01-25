using System.ComponentModel.DataAnnotations;
using Anet.Entity;

namespace Bookist.Entities;

public class Tag : Entity
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required, Varchar(20)]
    public string Name { get; set; }
    /// <summary>
    /// 处理后标题（用于构建URL）
    /// </summary>
    [Required, Varchar(20)]
    public string Slug { get; set; }

    public IEnumerable<Book> Books { get; set; }
}

