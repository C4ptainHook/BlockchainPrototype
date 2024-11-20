using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces;

public interface ITransactionService
{
    Task AddAsync(Transaction transaction);
    IEnumerable<Transaction> Get(int? numberOfTransactions = null);
    Task ClearAsync();
}
