using System.Security.Cryptography;
using System.Text;
using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Interfaces.Utils;
using Blockchain.Business.Models;
using Microsoft.Extensions.Logging;

namespace Blockchain.Business.Services;

public class ProofOfWorkService(
    IRandomNumerical<int> random,
    ProofOfWorkServiceArgs args,
    ILogger<IProofOfWorkService> logger
) : IProofOfWorkService
{
    private readonly IRandomNumerical<int> _random = random;
    private readonly ILogger<IProofOfWorkService> _logger = logger;

    public string? GetHash(in BlockModel? blockToProve)
    {
        if (blockToProve is null)
            return null;

        using var sha256 = SHA256.Create();
        var blockAsString = new StringBuilder();
        blockAsString
            .Append(blockToProve.Index)
            // .Append(blockToProve.TimeStamp)
            .Append(blockToProve.MerkleRoot)
            .Append(blockToProve.Proof)
            .Append(blockToProve.PreviousHash);

        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(blockAsString.ToString()));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    public bool IsHashValid(in string hashToCheck)
    {
        return hashToCheck.EndsWith(args.Difficulty);
    }

    public int GetNewNonce()
    {
        return _random.Next(args.NonceMaxValue);
    }
}
