using System.ComponentModel.DataAnnotations;

namespace Bookist.Web.Models;

public class Book
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; }

    [Required, MaxLength(255)]
    public string Cover { get; set; }

    [MaxLength(100)]
    public string Author { get; set; }

    public DateOnly PubDate { get; set; }

    [MaxLength(2000)]
    public string Description { get; set; }

    [Required, MaxLength(50)]
    public string Format { get; set; }

    [Required, MaxLength(100)]
    public string FetchUrl { get; set; }

    [MaxLength(10)]
    public string FetchCode { get; set; }
}