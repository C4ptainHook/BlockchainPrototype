using Blockchain.Data.Entities;

namespace Blockchain.Data.Interfaces;

public interface IBlockchainRepository<T> : IReadable<T>, IAddable<T>
    where T : BaseEntity
{ }
