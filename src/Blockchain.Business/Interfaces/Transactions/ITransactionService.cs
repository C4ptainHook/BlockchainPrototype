using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Transactions;

public interface ITransactionService
{
    Task<TransactionModel> AddAsync(TransactionModel transaction);
    Task<IEnumerable<TransactionModel>> Get(int? numberOfTransactions = null);
    Task ClearAsync();
}
