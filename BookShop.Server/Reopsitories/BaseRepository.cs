using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using BookShop.Server.Data;

namespace Generic.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;
    public BaseRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<T?> CreateAsync(T entity)
    {
        var addedEntity = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return addedEntity.Entity;
    }

    public virtual async Task<IQueryable<T>?> ReadAllAsync()
        => await Task.Run(() => _dbSet.AsNoTracking());

    public virtual async Task<IQueryable<T>?> ReadAllWithIncludesAsync(params string[] includes)
    {
        return await Task.Run
            (
                () =>
                {
                    var allEnities = _dbSet.AsQueryable();
                    foreach (var include in includes)
                    {
                        allEnities = allEnities.Include(include);
                    }
                    return allEnities;
                }
            );
    }

    public virtual async Task<T?> ReadByIdAsync(object id) => await _dbSet.FindAsync(id);

    public virtual async Task<T?> ReadByIdWithIncludesAsync(object id, params string[] includes)
    {
        var entity = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            entity = entity.Include(include);
        }
        var entit = await _dbSet.FindAsync(id);
        return await entity.FirstOrDefaultAsync(entity => entity == entit);
    }

    public virtual async Task<T?> UpdateAsync(Guid id, T updatedEntity)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity is null) return null;

        var poperties = typeof(T).GetProperties();

        foreach (var property in poperties)
        {
            if (property.Name == "Id") continue;

            var newValue = property.GetValue(updatedEntity);

            if (newValue is null) continue;
            property.SetValue(existingEntity, newValue);
        }

        await _context.SaveChangesAsync();

        return updatedEntity;
    }

    public virtual async Task<T?> DeleteAsync(Guid id)
    {
        var entityToRemove = await _dbSet.FindAsync(id);
        if (entityToRemove != null)
        {
            _dbSet.Remove(entityToRemove);
            await _context.SaveChangesAsync();
        }
        return entityToRemove;
    }

    public virtual async Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await Task.Run(() => _dbSet.Where(predicate));
    }
}