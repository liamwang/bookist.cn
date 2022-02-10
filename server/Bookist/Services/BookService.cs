using Anet;
using Anet.Data;
using Anet.Models;
using Bookist.Dtos;
using Bookist.Entities;
using Mapster;

namespace Bookist.Services;

public class BookService : ServiceBase<Db>
{
    public BookService(Db db) : base(db)
    {
        db.Connection.Open();
    }

    public async Task<PagedResult<BookDto>> GetAsync(int page, int size, string keyword = null, long? tagId = null)
    {
        var param = new SqlParams();

        var sql_ = Db.NewSql().From("Book").Where();
        if (!string.IsNullOrEmpty(keyword))
        {
            sql_.Line("AND (Title LIKE @pattern OR Subtitle LIKE @pattern OR Author LIKE @pattern)");
            param.Add("pattern", $"%{keyword}%");
        }
        if (tagId.HasValue)
        {
            sql_.Line("AND EXISTS (SELECT 1 FROM BookTag WHERE BookId=Book.Id AND TagId=@tagId)");
            param.Add("tagId", tagId.Value);
        }

        var result = new PagedResult<BookDto>(page, size)
        {
            Total = await Db.SingleAsync<int>(sql_.Select("COUNT(1)"), param),
            Items = await Db.QueryAsync<BookDto>(
                sql_.Select("*").Order("Id DESC").Page(page, size), param),
        };

        sql_.Clear()
            .Line("SELECT * FROM BookTag BT")
            .Line("LEFT JOIN Tag T ON T.Id=BT.TagId")
            .Line("WHERE BT.BookId IN @bookIds");
        var bookTags = await Db.QueryAsync<BookTag, Tag, BookTag>(sql_,
            (bookTag, tag) =>
            {
                bookTag.Tag = tag;
                return bookTag;
            },
            new { bookIds = result.Items.Select(x => x.Id) });

        foreach (var book in result.Items)
        {
            book.Tags = bookTags.Where(x => x.BookId == book.Id).Select(x => x.Tag).ToList();
        }

        return result;
    }

    public async Task<BookDto> GetByIdAsync(long id)
    {
        var sql_ = Db.NewSql();

        sql_.Line("SELECT * FROM Book WHERE Id=@id");
        var book = await Db.FirstOrDefaultAsync<BookDto>(sql_, new { id });

        NotFoundException.ThrowIf(book == null);

        sql_.Clear();
        sql_.Line("SELECT T.*")
            .Line("FROM BookTag BT")
            .Line("LEFT JOIN Tag T ON T.Id=BT.TagId")
            .Line("WHERE BT.BookId=@id");
        book.Tags = (await Db.QueryAsync<Tag>(sql_, new { id })).ToList();

        return book;
    }

    public async Task SaveAsync(BookEditDto dto)
    {
        using var tran = Db.BeginTransaction();

        // Handle Books
        Book book;
        if (dto.Id == 0)
        {
            book = dto.Adapt<Book>();
            await Db.InsertAsync(book);
        }
        else
        {
            book = await Db.FindAsync<Book>(new { dto.Id });
            dto.Adapt(book);
            await Db.UpdateAsync(book);
        }

        // Handle Tags
        var existTags = Db.Query<Tag>(
            "SELECT * FROM Tag WHERE Name IN @Tags", new { dto.Tags });
        var newTags = dto.Tags
            .Where(x => !existTags.Any(y => y.Name == x))
            .Select(x => new Tag { Id = IdGen.NewId(), Name = x }).ToList();
        //foreach (var tag in newTags)
        //{
        //    tag.Id = IdGen.NewId();
        //}
        await Db.InsertBatchAsync(newTags);

        // Handle BookTags
        var bookTags = existTags.Concat(newTags)
            .Select(x => new BookTag { BookId = book.Id, TagId = x.Id });
        await Db.DeleteAsync<BookTag>(new { BookId = dto.Id });
        await Db.InsertBatchAsync(bookTags);

        tran.Commit();
    }

    public async Task DeleteAsync(long id)
    {
        var rows = await Db.DeleteAsync("Book", new { Id = id });
        if (rows == 0)
        {
            throw new NotFoundException();
        }
    }
}
