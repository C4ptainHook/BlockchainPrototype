using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Transactions;

public interface ITransactionService
{
    Task<TransactionModel> AddAsync(TransactionModel transaction);
    Task<IEnumerable<TransactionModel>> GetAttachedToTheBlock(BlockModel block = null!);
    Task UpdateAsync(TransactionModel transaction);
}
