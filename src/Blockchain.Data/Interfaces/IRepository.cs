namespace Blockchain.Data.Interfaces;

public interface IRepository<T>
    where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
}
