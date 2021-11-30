using Bookist.Web.Models;

namespace Bookist.Web.ViewModels;

public class BookListVM
{
    public IEnumerable<Book> Books { get; set; }
    public PagerVM Pager { get; set; }
}
