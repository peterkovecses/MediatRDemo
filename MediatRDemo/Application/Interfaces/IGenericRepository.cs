using System.Linq.Expressions;

namespace MediatRDemo.Application.Interfaces;

public interface IGenericRepository<TEntity, TId> where TEntity : class
{
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = default, CancellationToken cancellationToken = default);

    Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    void Remove(TEntity entity);
}
