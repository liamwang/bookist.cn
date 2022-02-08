using Bookist.Services;
using Bookist.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Controllers;

public class BookController : Controller
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<ViewResult> Index(int pageNo = 1, int pageSize = 12)
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

    public async Task<ActionResult> Detail(string slug)
    {
        if (UrlUtil.ResolveIdInSlug(slug, out var id))
        {
            var vm = await _bookService.GetByIdAsync(id);
            ViewData["Description"] = vm.Intro;
            return View(vm);
        }
        return NotFound();
    }
}
