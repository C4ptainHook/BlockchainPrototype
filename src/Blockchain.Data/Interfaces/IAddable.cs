namespace Blockchain.Data.Interfaces;

public interface IAddable<T>
{
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
}
