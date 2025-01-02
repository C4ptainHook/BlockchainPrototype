using System.Security.Cryptography;
using System.Text;
using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Models;

namespace Blockchain.Business.Services;

public class TransactionHashingService : ITransactionHashingService
{
    private readonly SHA256 _sha256 = SHA256.Create();

    public string GetMerkleRoot(IEnumerable<TransactionModel> transactions)
    {
        var transactionHashes = transactions.Select(GetSingleHash).OrderBy(th => th).ToList();
        if (transactionHashes.Count == 0)
            return string.Empty;

        var newHashes = new Queue<string>(transactionHashes);
        while (newHashes.Count > 1)
        {
            var countSnapshot = newHashes.Count;
            for (var i = 0; i < countSnapshot; i += 2)
            {
                var firstHash = newHashes.Dequeue();
                var secondHash = i + 1 < countSnapshot ? newHashes.Dequeue() : string.Empty;
                var newHash = _sha256.ComputeHash(Encoding.UTF8.GetBytes(firstHash + secondHash));
                newHashes.Enqueue(BitConverter.ToString(newHash).Replace("-", "").ToLower());
            }
        }
        return newHashes.First();
    }

    public string GetSingleHash(TransactionModel transaction)
    {
        var transactionAsString = new StringBuilder();
        transactionAsString
            .Append(transaction.Id)
            .Append(transaction.SenderWallet)
            .Append(transaction.RecipientWallet)
            .Append(transaction.Amount)
            .Append(transaction.TimeStamp);

        var hashBytes = _sha256.ComputeHash(Encoding.UTF8.GetBytes(transactionAsString.ToString()));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
