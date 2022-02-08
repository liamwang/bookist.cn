using Anet.Models;
using Bookist.Dtos;
using Bookist.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Api;

[ApiAuth]
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<PagedResult<BookDto>> Get(
        int page = 1, int size = 10, string keyword = null)
    {
        return await _bookService.GetAsync(page, size, keyword);
    }

    [HttpGet("{id}")]
    public Task<BookDto> GetById(long id)
    {
        return _bookService.GetByIdAsync(id);
    }

    [HttpPost]
    public Task Save(BookEditDto dto)
    {
        return _bookService.SaveAsync(dto);
    }

    [HttpDelete("{id}")]
    public Task Delete(long id)
    {
        return _bookService.DeleteAsync(id);
    }
}
