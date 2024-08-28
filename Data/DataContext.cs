using System;
using Microsoft.EntityFrameworkCore;
using Memory.Entities;

namespace Memory.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Card> Card {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>()
            .HasMany(g => g.Field)
            .WithOne()
            .HasForeignKey("GameId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Card>()
            .HasKey(c => c.Id);
    }
}
