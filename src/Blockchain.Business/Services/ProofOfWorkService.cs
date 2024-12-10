using System.Security.Cryptography;
using System.Text;
using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Interfaces.Utils;
using Blockchain.Business.Models;
using Microsoft.Extensions.Logging;

namespace Blockchain.Business.Services;

public class ProofOfWorkService : IProofOfWorkService
{
    private readonly IRandomNumerical<int> _random;
    private readonly ProofOfWorkServiceArgs _args;
    private readonly ICultureService _cultureService;

    public ProofOfWorkService(
        ICultureService cultureService,
        IRandomNumerical<int> random,
        ProofOfWorkServiceArgs args
    )
    {
        _random = random;
        _args = args;
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
        using var sha256 = SHA256.Create();
        for (var i = 0; i < transactionHashes.Count; i += 2)
        {
            var firstHash = transactionHashes[i];
            var secondHash =
                i + 1 < transactionHashes.Count ? transactionHashes[i + 1] : string.Empty;
            var newHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(firstHash + secondHash));
            newHashes.Add(BitConverter.ToString(newHash).Replace("-", "").ToLower());
        }
        return GetMerkleRoot(newHashes);
    }

    public string? GetHash(in BlockModel? blockToProve)
    {
        if (blockToProve is null)
            return null;

        using var sha256 = SHA256.Create();
        var blockAsString = new StringBuilder();
        blockAsString
            .Append(blockToProve.Index)
            .Append(blockToProve.TimeStamp.ToString("o", _cultureService.CurrentCulture))
            .Append(blockToProve.MerkleRoot)
            .Append(blockToProve.Proof)
            .Append(blockToProve.PreviousHash);

        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(blockAsString.ToString()));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    public bool IsHashValid(in string hashToCheck)
    {
        return hashToCheck.EndsWith(_args.Difficulty);
    }

    public int GetNewNonce()
    {
        return _random.Next(_args.NonceMaxValue);
    }
}
