using Bookist.Services;
using Bookist.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Controllers;

public class BookController : Controller
{
    private const int _pageSize = 12;

    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<ViewResult> Index(
        int p = 1, string q = null, long? tid = null)
    {
        var pageData = await _bookService.GetAsync(p, _pageSize, q, tid);
        var vm = new BookListVM()
        {
            Books = pageData.Items,
            Pager = new()
            {
                PageNo = p,
                PageSize = _pageSize,
                TotalItems = pageData.Total
            },
            CurrentKeyword = q
        };
        if (tid.HasValue)
        {
            vm.CurrentTag = pageData.Items.FirstOrDefault()?.Tags.First().Name;
        }
        return View(vm);
    }

    public ActionResult Search([FromForm] string q = null)
    {
        return RedirectToAction("Index", new { q });
    }

    public async Task<ActionResult> Detail(long id)
    {
        var vm = await _bookService.GetByIdAsync(id);
        return View(vm);
    }
}
