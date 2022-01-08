﻿using Bookist.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookist;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>();
    }
}