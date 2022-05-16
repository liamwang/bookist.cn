using System.ComponentModel.DataAnnotations;
using Anet.Entity;

namespace GeekGist.Entities;

public class Tag : Entity
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required, Varchar(20)]
    public string Name { get; set; }

    public IEnumerable<Book> Books { get; set; }
}

