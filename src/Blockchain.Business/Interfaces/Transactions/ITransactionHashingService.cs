using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Transactions;

public interface ITransactionHashingService
{
    string GetSingleHash(TransactionModel transaction);
    string GetMerkleRoot(IEnumerable<string> transactionsHashes);
}
