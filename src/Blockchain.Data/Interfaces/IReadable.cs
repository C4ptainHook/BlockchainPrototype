namespace Blockchain.Data.Interfaces;

public interface IReadable<T>
    where T : class
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
}
