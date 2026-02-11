using DAL.Context;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class
{
    protected readonly WardrobeDbContext context;

    public GenericRepository(WardrobeDbContext context)
    {
        this.context = context;
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        var entity = await context.Set<T>().FindAsync(id)
                     ?? throw new KeyNotFoundException($"{typeof(T).Name} with ID {id} not found.");
        return entity;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync()
               ?? throw new KeyNotFoundException($"{typeof(T).Name}s is empty.");
    }

    public virtual async Task AddAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(entity)} entity cannot be null.");
        }
        await context.Set<T>().AddAsync(entity);
    }

    public virtual async Task UpdateAsync(T? entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(entity)} entity cannot be null.");
        }
        await Task.Run(() => context.Set<T>().Update(entity));
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"{nameof(T)} with ID {id} not found.");
        await Task.Run(() => context.Set<T>().Remove(entity));
    }
}