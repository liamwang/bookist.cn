using Bookist.Services;
using Bookist.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Controllers;

public class BookController : Controller
{
    const int _pageSize = 12;

    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<ViewResult> Index(
        int p = 1, string keyword = null, long? tagId = null)
    {
        var pageData = await _bookService.GetAsync(p, _pageSize, keyword, tagId);
        var vm = new BookListVM()
        {
            Books = pageData.Items,
            Pager = new()
            {
                PageNo = p,
                PageSize = _pageSize,
                TotalItems = pageData.Total
            }
        };
        return View(vm);
    }

    public async Task<ActionResult> Detail(long id)
    {
        var vm = await _bookService.GetByIdAsync(id);
        ViewData["Description"] = vm.Intro;
        return View(vm);
    }
}
