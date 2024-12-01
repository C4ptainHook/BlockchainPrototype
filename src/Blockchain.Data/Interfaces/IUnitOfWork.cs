namespace Blockchain.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    BlockchainContext Context { get; }
    Task CommitAsync();
    Task RollbackAsync();
}
