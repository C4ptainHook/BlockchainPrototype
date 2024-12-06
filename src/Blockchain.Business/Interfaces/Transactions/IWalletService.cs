using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Transactions;

public interface IWalletService
{
    Task AddAsync(WalletModel wallet);
    Task<string> GetIdByNickNameAsync(string nickName);
}
