using Anet.Models;
using Bookist.Dtos;
using Bookist.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Api;

[Authorize]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("[action]")]
    public async Task<PagedResult<BookDto>> Get(int page, int size, string keyword = null)
    {
        return await _bookService.GetAsync(page, size, keyword);
    }
}
