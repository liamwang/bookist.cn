using Bookist.Entities;
using Microsoft.EntityFrameworkCore;

namespace Migrator;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>(b =>
        {
            b.Property(x => x.Id).ValueGeneratedNever();
            b.HasMany(x => x.Tags).WithMany(x => x.Books).UsingEntity<BookTag>(
                j => j.HasOne(x => x.Tag).WithMany().HasForeignKey(x => x.TagId),
                j => j.HasOne(x => x.Book).WithMany().HasForeignKey(x => x.BookId),
                j => { j.HasKey(x => new { x.BookId, x.TagId }); }
            );
        });
        builder.Entity<Tag>(b =>
        {
            b.Property(x => x.Id).ValueGeneratedNever();
        });
    }
}
