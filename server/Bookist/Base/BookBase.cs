using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookist.Base;

public abstract class BookBase
{
    [DisplayName("名称")]
    [Required, MaxLength(100)]
    public string Title { get; set; }

    [DisplayName("封面")]
    [Required, MaxLength(255)]
    public string Cover { get; set; }

    [DisplayName("作者")]
    [MaxLength(100)]
    public string Author { get; set; }

    [DisplayName("出版年月")]
    [Column(TypeName = "date")]
    public DateTime PubDate { get; set; }

    [DisplayName("描述")]
    [MaxLength(2000)]
    public string Description { get; set; }

    [DisplayName("文件格式")]
    [Required, MaxLength(50)]
    public string Format { get; set; }

    [DisplayName("提取链接")]
    [Required, MaxLength(100)]
    public string FetchUrl { get; set; }

    [DisplayName("提取码")]
    [MaxLength(10)]
    public string FetchCode { get; set; }
}
