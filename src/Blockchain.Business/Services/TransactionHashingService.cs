using System.Security.Cryptography;
using System.Text;
using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Interfaces.Utils;
using Blockchain.Business.Models;

namespace Blockchain.Business.Services;

public class TransactionHashingService : ITransactionHashingService
{
    private readonly SHA256 _sha256 = SHA256.Create();
    private readonly ICultureService _cultureService;

    public TransactionHashingService(ICultureService cultureService)
    {
        _cultureService = cultureService;
    }

    public string GetMerkleRoot(IEnumerable<string> transactionsHashes)
    {
        var transactionHashes = transactionsHashes.OrderBy(th => th).ToList();
        if (transactionHashes.Count == 0)
            return string.Empty;
        if (transactionHashes.Count == 1)
            return transactionHashes[0];
        var newHashes = new List<string>();
        for (var i = 0; i < transactionHashes.Count; i += 2)
        {
            var firstHash = transactionHashes[i];
            var secondHash =
                i + 1 < transactionHashes.Count ? transactionHashes[i + 1] : string.Empty;
            var newHash = _sha256.ComputeHash(Encoding.UTF8.GetBytes(firstHash + secondHash));
            newHashes.Add(BitConverter.ToString(newHash).Replace("-", "").ToLower());
        }
        return GetMerkleRoot(newHashes);
    }

    public string GetSingleHash(TransactionModel transaction)
    {
        var transactionAsString = new StringBuilder();
        transactionAsString
            .Append(transaction.Id)
            .Append(transaction.Sender)
            .Append(transaction.Recipient)
            .Append(transaction.Amount)
            .Append(transaction.TimeStamp.ToString("o", _cultureService.CurrentCulture));

        var hashBytes = _sha256.ComputeHash(Encoding.UTF8.GetBytes(transactionAsString.ToString()));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
