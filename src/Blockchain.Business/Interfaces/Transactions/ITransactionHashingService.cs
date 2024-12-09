using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Transactions;

public interface ITransactionHashingService
{
    string GetTransactionHash(TransactionModel transaction);
    string GetMerkleRoot(IEnumerable<string> transactionsHashes);
}
