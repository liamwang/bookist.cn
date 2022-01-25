using Bookist.Dtos;

namespace Bookist.Web.Models;

public class BookListVM
{
    public IEnumerable<BookDto> Books { get; set; }
    public PagerVM Pager { get; set; }
}
