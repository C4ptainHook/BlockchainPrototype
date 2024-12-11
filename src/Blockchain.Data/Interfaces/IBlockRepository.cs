using Blockchain.Data.Entities;

namespace Blockchain.Data.Interfaces;

public interface IBlockRepository<T> : IReadable<T>, IAddable<T>
    where T : BaseEntity
{ }
