using Bookist.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Api;

[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }
}
