namespace Blockchain.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    T GetRepository<T>()
        where T : class;
    Task CommitAsync();
}
