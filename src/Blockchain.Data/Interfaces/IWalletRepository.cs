using Blockchain.Data.Entities;

namespace Blockchain.Data.Interfaces;

public interface IWalletRepository<T> : IReadable<T>, IAddable<T>, IRemovable<T>
    where T : BaseEntity
{
    Task<Wallet> GetByNickNameAsync(string nickName);
}
