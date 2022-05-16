using GeekGist.Dtos;

namespace GeekGist.Web.Models;

public class BookListVM
{
    public IEnumerable<BookDto> Books { get; set; }
    public PagerVM Pager { get; set; }
    public string CurrentTag { get; set; }
    public string CurrentKeyword { get; set; }
}
