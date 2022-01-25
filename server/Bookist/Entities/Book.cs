using System.ComponentModel.DataAnnotations;
using Anet.Entity;

namespace Bookist.Entities;

public class Book : AuditEntity
{
    /// <summary>名称</summary>
    [Required, Varchar(127)]
    public string Name { get; set; }

    /// <summary>
    /// 处理后标题（用于构建URL）
    /// </summary>
    [Required, Varchar(127)]
    public string Slug { get; set; }

    /// <summary>封面（文件Key）</summary>
    [Required, Varchar(255)]
    public string Cover { get; set; }

    /// <summary>作者</summary>
    [Varchar(100)]
    public string Author { get; set; }

    /// <summary>出版年月</summary>
    [Date]
    public DateTime PubDate { get; set; }

    /// <summary>描述</summary>
    [Required, Text]
    public string Intro { get; set; }

    /// <summary>文件格式（用“/”分隔多个）</summary>
    [Required, Varchar(50)]
    public string Formats { get; set; }

    /// <summary>提取链接</summary>
    [Required, Varchar(127)]
    public string FetchUrl { get; set; }

    /// <summary>提取码</summary>
    [MaxLength(10)]
    public string FetchCode { get; set; }

    /// <summary>阅读数</summary>
    public int Views { get; set; }

    /// <summary>下载数</summary>
    public int Downloads { get; set; }

    public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();
}