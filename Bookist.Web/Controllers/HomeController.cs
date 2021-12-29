using Bookist.Web.Entities;
using Bookist.Web.Models;
using Bookist.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookist.Web.Controllers;

public class HomeController : Controller
{
    private readonly BookistDbContext _context;

    private readonly DbSet<Book> _books;

    public HomeController(BookistDbContext context)
    {
        _context = context;
        _books = context.Set<Book>();
    }

    public async Task<ViewResult> Index(int page = 1, int size = 5)
    {
        BookListVM vm = new()
        {
            Books = await _books
                .OrderByDescending(x => x.Id)
                .Page(page, size)
                .ToListAsync(),
            Pager = new PagerVM
            {
                Page = page,
                Size = size,
                TotalItems = await _books.CountAsync()
            }
        };
        return View(vm);
    }
}
