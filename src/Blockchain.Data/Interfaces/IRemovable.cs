namespace Blockchain.Data.Interfaces;

public interface IRemovable<T>
    where T : class
{
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
