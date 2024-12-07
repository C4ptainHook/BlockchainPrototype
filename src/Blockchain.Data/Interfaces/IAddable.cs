namespace Blockchain.Data.Interfaces;

public interface IAddable<T>
{
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
}
