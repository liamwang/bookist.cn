using Microsoft.EntityFrameworkCore;

namespace Bookist.Web.Models;

public class BookistDbContext : DbContext
{
    public BookistDbContext(DbContextOptions<BookistDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>();
    }
}
