using System.Linq.Expressions;
using QRisto.Persistence.Entity;

namespace QRisto.Persistence.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class, IEntity
{
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");

    Task<TEntity> GetByIdAsync(object id);

    Task<TEntity> InsertAsync(TEntity entity);

    Task SaveAsync();

    Task DeleteAsync(object id, Guid userId);

    void Delete(TEntity entityToDelete, Guid userId);

    void Update(TEntity entityToUpdate);
}