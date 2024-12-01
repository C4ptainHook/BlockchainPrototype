namespace Blockchain.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Dictionary<string, Type> Repositories { get; }
    Task CommitAsync();
}
