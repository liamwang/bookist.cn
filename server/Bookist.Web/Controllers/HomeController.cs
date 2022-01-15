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
        var pageData = await _bookService
            .GetPageAsync(pageNo, pageSize);
        BookListVM vm = new()
        {
            Books = pageData.Items,
            Pager = new()
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalItems = pageData.Total
            }
        };
        return View(vm);
    }
}
