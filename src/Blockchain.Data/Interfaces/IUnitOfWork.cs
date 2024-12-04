namespace Blockchain.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    T GetRepository<T>(string repositoryName)
        where T : class;
    Task CommitAsync();
}
