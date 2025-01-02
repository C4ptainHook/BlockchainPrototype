using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Transactions;

public interface IWalletService
{
    Task AddAsync(WalletModel wallet);
    Task<WalletModel?> GetByNickNameAsync(string nickName);
    Task<WalletModel?> GetByIdAsync(string id);
    Task UpdateAsync(WalletModel wallet);
}
