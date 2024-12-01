namespace Blockchain.Data.Interfaces;

public interface IReadable<T>
    where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}
