using MediatRDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatRDemo.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasData(
                new Movie { Id = 1, Title = "Forrest Gump", ReleaseYear = 1994 },
                new Movie { Id = 2, Title = "Pulp Fiction", ReleaseYear = 1994 },
                new Movie { Id = 3, Title = "The Shawshank Redemption", ReleaseYear = 1994 },
                new Movie { Id = 4, Title = "The Dark Knight", ReleaseYear = 2008 },
                new Movie { Id = 5, Title = "Fight Club", ReleaseYear = 1999 },
                new Movie { Id = 6, Title = "The Godfather", ReleaseYear = 1972 },
                new Movie { Id = 7, Title = "Titanic", ReleaseYear = 1997 },
                new Movie { Id = 8, Title = "Inception", ReleaseYear = 2010 },
                new Movie { Id = 9, Title = "The Matrix", ReleaseYear = 1999 },
                new Movie { Id = 10, Title = "Schindler's List", ReleaseYear = 1993 }
            );
    }
}
