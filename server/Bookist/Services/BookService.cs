using Bookist.Entities;
using Bookist.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookist.Services;

public class BookService : ServiceBase<AppDbContext, Book>
{
    public BookService(AppDbContext db) : base(db)
    {
    }

    public async Task<PagedResult<Book>> GetPageAsync(int page, int size)
    {
        return new PagedResult<Book>(page, size)
        {
            List = await Entities.AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Page(page, size)
                .ToListAsync(),
            Total = await Entities.CountAsync()
        };
    }
}
