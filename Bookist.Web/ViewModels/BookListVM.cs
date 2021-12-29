using Bookist.Entities;

namespace Bookist.Web.ViewModels;

public class BookListVM
{
    public IEnumerable<Book> Books { get; set; }
    public PagerVM Pager { get; set; }
}
