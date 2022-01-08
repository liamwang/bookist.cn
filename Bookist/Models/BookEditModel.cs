using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookist.Models;

public class BookEditModel : BookBase
{
    [Required(ErrorMessage = "出版年月不能为空"), MaxLength(50)]
    public DateTime? PubDate { get; set; }
}
