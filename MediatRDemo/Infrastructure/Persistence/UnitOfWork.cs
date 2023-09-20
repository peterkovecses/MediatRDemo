using MediatRDemo.Application.Interfaces;
using MediatRDemo.Infrastructure.Persistence.Repositories;

namespace MediatRDemo.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Lazy<IMovieRepository> _movies;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _movies = new(() => new MovieRepository(_context));
    }

    public IMovieRepository Movies => _movies.Value;

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    private bool _disposed = false;

    ~UnitOfWork() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
}
