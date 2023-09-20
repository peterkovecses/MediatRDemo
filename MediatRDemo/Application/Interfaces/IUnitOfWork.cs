namespace MediatRDemo.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IMovieRepository Movies { get; }

    Task<int> CompleteAsync();
}
