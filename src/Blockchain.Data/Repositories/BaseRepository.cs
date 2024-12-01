using Blockchain.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _entities;

    protected BaseRepository(DbSet<T> entities)
    {
        _entities = entities;
    }

    public virtual async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.AsNoTracking().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _entities.FindAsync(id)
            ?? throw new KeyNotFoundException($"{typeof(T).Name} entity with id:{id} not found");
    }
}
