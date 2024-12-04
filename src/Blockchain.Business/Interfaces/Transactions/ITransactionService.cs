using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces;

public interface ITransactionService
{
    Task AddAsync(TransactionModel transaction);
    Task<IEnumerable<TransactionModel>> Get(int? numberOfTransactions = null);
    Task ClearAsync();
}
