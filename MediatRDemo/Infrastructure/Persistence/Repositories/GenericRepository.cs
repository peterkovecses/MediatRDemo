using MediatRDemo.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MediatRDemo.Infrastructure.Persistence.Repositories;

public abstract class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : class
{
    protected readonly DbContext _context;

    public GenericRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = default, CancellationToken cancellationToken = default)
    {
        var query = _context
            .Set<TEntity>()
            .AsQueryable();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken)
        => await _context.Set<TEntity>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

    public void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);
}
