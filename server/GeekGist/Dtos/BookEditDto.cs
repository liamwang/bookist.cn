using System.ComponentModel.DataAnnotations;
using Anet.Entity;

namespace GeekGist.Dtos;

public class BookEditDto
{
    public long Id { get; set; }

    /// <summary>标题</summary>
    [Required, Varchar(127)]
    public string Title { get; set; }

    /// <summary>子标题</summary>
    [Varchar(127)]
    public string Subtitle { get; set; }

    /// <summary>封面（文件Key）</summary>
    [Required, Varchar(255)]
    public string Cover { get; set; }

    /// <summary>作者</summary>
    [Varchar(100)]
    public string Author { get; set; }

    /// <summary>ISBN</summary>
    [Varchar(20)]
    public string Isbn { get; set; }

    /// <summary>出版年月</summary>
    [Date]
    public DateTime PubDate { get; set; }

    /// <summary>描述</summary>
    [Required]
    public string Intro { get; set; }

    /// <summary>官方链接</summary>
    [Varchar(127)]
    public string OrgUrl { get; set; }

    /// <summary>提取链接</summary>
    [Required, Varchar(127)]
    public string FetchUrl { get; set; }

    /// <summary>提取码</summary>
    [Varchar(10)]
    public string FetchCode { get; set; }

    /// <summary>文件格式</summary>
    [Required]
    public string[] Formats { get; set; }

    /// <summary>标签</summary>
    [Required]
    public string[] Tags { get; set; }
}

