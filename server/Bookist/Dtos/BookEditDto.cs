using System.ComponentModel.DataAnnotations;
using Anet.Entity;

namespace Bookist.Dtos;

public class BookEditDto 
{
    public long Id { get; set; }

    /// <summary>名称</summary>
    [Required, Varchar(127)]
    public string Name { get; set; }

    /// <summary>封面（文件Key）</summary>
    [Required, Varchar(255)]
    public string Cover { get; set; }

    /// <summary>作者</summary>
    [MaxLength(100)]
    public string Author { get; set; }

    /// <summary>出版年月</summary>
    [Date]
    public DateTime PubDate { get; set; }

    /// <summary>描述</summary>
    [Required]
    public string Intro { get; set; }

    /// <summary>提取链接</summary>
    [Required, MaxLength(127)]
    public string FetchUrl { get; set; }

    /// <summary>提取码</summary>
    [MaxLength(10)]
    public string FetchCode { get; set; }

    /// <summary>文件格式</summary>
    [Required]
    public string[] Formats { get; set; }

    /// <summary>标签</summary>
    [Required]
    public string[] Tags { get; set; }
}

