using Blockchain.Data.Entities;

namespace Blockchain.Data.Interfaces;

public interface ITransactionRepository<T> : IReadable<T>, IAddable<T>, IRemovable<T>
    where T : BaseEntity
{ }
