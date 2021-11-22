using Bookist.Web.Models;
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

    public async Task<ViewResult> Index()
    {
        var books = await _books.ToListAsync();
        return View(books);
    }
}
