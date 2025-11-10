using System;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    public DbSet<Expositante> Expositante => Set<Expositante>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Feedback>()
            .HasIndex(f => new { f.BrowserId, f.AreaId })
            .IsUnique();
    }

}
