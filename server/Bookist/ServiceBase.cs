using Microsoft.EntityFrameworkCore;

namespace Bookist.Inernal;

public abstract class ServiceBase<TDbContext, TEntity>
    where TDbContext : DbContext
    where TEntity : class
{
    public ServiceBase(TDbContext db)
    {
        Db = db;
        Entities = db.Set<TEntity>();
    }

    protected TDbContext Db { get; }

    protected DbSet<TEntity> Entities { get; }
}
