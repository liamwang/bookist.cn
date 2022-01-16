using Bookist.Services;
using Bookist.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Controllers;

public class HomeController : Controller
{
    private readonly BookService _bookService;

    public HomeController(BookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<ViewResult> Index(int pageNo = 1, int pageSize = 5)
    {
        var page = await _bookService.GetAsync(pageNo, pageSize);
        var vm = new BookListVM()
        {
            Books = page.Items,
            Pager = new()
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalItems = page.Total
            }
        };
        return View(vm);
    }
}
