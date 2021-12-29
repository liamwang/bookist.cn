using Bookist.Services;
using Bookist.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Controllers;

public class HomeController : Controller
{
    private readonly BookService _bookService;

    public HomeController(BookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<ViewResult> Index(int page = 1, int size = 5)
    {
        var pageData = await _bookService
            .GetPageAsync(page, size);
        BookListVM vm = new()
        {
            Books = pageData.List,
            Pager = new()
            {
                Page = page,
                Size = size,
                TotalItems = pageData.Total
            }
        };
        return View(vm);
    }
}
