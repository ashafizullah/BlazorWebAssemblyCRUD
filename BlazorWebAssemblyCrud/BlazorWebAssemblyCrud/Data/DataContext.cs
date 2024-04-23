using BlazorWebAssembly.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAssemblyCrud.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VideoGame>().HasData(
            new VideoGame { Id = 1, Title = "Super Mario Bros.", Publisher = "Nintendo", ReleaseYear = 1985 },
            new VideoGame { Id = 2, Title = "The Legend of Zelda", Publisher = "Nintendo", ReleaseYear = 1986 },
            new VideoGame { Id = 3, Title = "Testing", Publisher = "Testing", ReleaseYear = 2020 }
        );
    }

    public DbSet<VideoGame> VideoGames { get; set; }
}