using MediatRDemo.Domain.Entities;

namespace MediatRDemo.Application.Interfaces;

public interface IMovieRepository : IGenericRepository<Movie, int>
{
}
