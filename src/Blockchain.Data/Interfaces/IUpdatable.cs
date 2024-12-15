namespace Blockchain.Data.Interfaces;

public interface IUpdatable<T>
    where T : class
{
    void Update(T entity);
}
