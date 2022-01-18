using Anet;
using Anet.Data;
using Anet.Models;
using Bookist.Dtos;

namespace Bookist.Services;

public class BookService : ServiceBase<Db>
{
    public BookService(Db db) : base(db)
    {
    }

    public async Task<PagedResult<BookDto>> GetAsync(int page, int size, string keyword = null)
    {
        var param = new SqlParams();

        var sql = Db.NewSql().Select("*").From("Book").Where();
        if (!string.IsNullOrEmpty(keyword))
        {
            sql.LineTab("AND (Name LIKE @pattern OR Author LIKE @pattern) ");
            param.Add("pattern", $"'%{keyword}'%");
        }

        var result = new PagedResult<BookDto>(page, size)
        {
            Items = await Db.QueryAsync<BookDto>(sql.OrderByDesc("Id").Page(page, size), param),
            Total = await Db.QuerySingleAsync<int>(sql.DePage().Count(), param),
        };

        return result;
    }

    public async Task CreateAsync(BookEditDto dto)
    {
        await Db.InsertAsync(dto, "Book");
    }

    public async Task UpdateAsync(int id, BookEditDto dto)
    {
        var rows = await Db.UpdateAsync("Book", dto, new { Id = id });
        if (rows == 0) throw new NotFoundException();
    }

    public async Task DeleteAsync(int id)
    {
        var rows = await Db.DeleteAsync("Book", new { Id = id });
        if (rows == 0) throw new NotFoundException();
    }
}
