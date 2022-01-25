using Anet;
using Anet.Data;
using Bookist.Dtos;
using Bookist.Entities;
using Mapster;

namespace Bookist.Services;

public class TagService : ServiceBase<Db>
{
    public TagService(Db db) : base(db)
    {
    }

    public async Task<IEnumerable<TagDto>> GetAsync(string keyword = null)
    {
        var param = new SqlParams();
        var sql = Db.NewSql()
            .Line("SELECT T.*, COUNT(BT.BookId) BookCount")
            .Line("FROM Tag T")
            .Line("LEFT JOIN BookTag BT ON T.Id=BT.TagId")
            .Line("WHERE 1=1");

        if (!string.IsNullOrEmpty(keyword))
        {
            sql.LineTab("AND (Name LIKE @pattern) ");
            param.Add("pattern", $"%{keyword}%");
        }

        sql.Line("GROUP BY T.Id");

        var list = await Db.QueryAsync<TagDto>(sql, param);

        return list;
    }

    public async Task SaveAsync(TagEditDto dto)
    {
        using var tran = Db.BeginTransaction();

        var existSlug = await Db.FirstOrDefaultAsync<string>(
            "SELECT Slug FROM Tag WHERE Slug=@Slug AND Id<>@Id",
            new { dto.Slug, dto.Id });
        if (existSlug != null)
        {
            throw new BadRequestException("标签Slug已存在！");
        }

        if (dto.Id == 0) // Insert
        {
            var tag = dto.Adapt<Tag>();
            tag.Id = IdGen.NewId();
            await Db.InsertAsync(tag);
        }
        else // Update
        {
            var tag = await Db.FindAsync<Tag>(new { dto.Id });
            dto.Adapt(tag);
            await Db.UpdateAsync(tag);
        }
        tran.Commit();
    }

    public async Task DeleteAsync(long id)
    {
        var rows = await Db.DeleteAsync("Tag", new { Id = id });
        if (rows == 0)
        {
            throw new NotFoundException();
        }
    }
}
