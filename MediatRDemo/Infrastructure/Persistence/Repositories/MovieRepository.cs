using MediatRDemo.Application.Interfaces;
using MediatRDemo.Domain.Entities;

namespace MediatRDemo.Infrastructure.Persistence.Repositories;

public class MovieRepository : GenericRepository<Movie, int>, IMovieRepository
{
    public MovieRepository(AppDbContext context) : base(context)
    {
    }

    public AppDbContext AppDbContext
        => (_context as AppDbContext)!;
}
