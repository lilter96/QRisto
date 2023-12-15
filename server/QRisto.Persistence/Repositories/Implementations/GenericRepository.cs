using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity;

namespace QRisto.Persistence.Repositories.Implementations;

public class GenericRepository<TEntity> where TEntity : class, IEntity
{
    internal readonly ApplicationDbContext Context;
    internal readonly DbSet<TEntity> DbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = includeProperties.Split(
                new[] { ',' },
                StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(
                query,
                (current, includeProperty) => current.Include(includeProperty));

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(object id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        return entity;
    }

    public async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(object id)
    {
        var entityToDelete = await DbSet.FindAsync(id);
        Delete(entityToDelete);
    }

    public void Delete(TEntity entityToDelete)
    {
        if (Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            DbSet.Attach(entityToDelete);
        }

        DbSet.Remove(entityToDelete);
    }

    public void Update(TEntity entityToUpdate)
    {
        DbSet.Attach(entityToUpdate);
        Context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}