using System.ComponentModel.DataAnnotations;

namespace Bookist.Models;

public class BookEditDto : BookBase
{
    [Required(ErrorMessage = "出版年月不能为空"), MaxLength(50)]
    public new DateOnly? PubDate { get; set; }
}
