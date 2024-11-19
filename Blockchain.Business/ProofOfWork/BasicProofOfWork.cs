﻿using System.Security.Cryptography;
using System.Text;
using Blockchain.Business.CryptoChain;
using Blockchain.Business.RandomWrappers;
using Microsoft.Extensions.Logging;

namespace Blockchain.Business.ProofOfWork;

public class BasicProofOfWork(IRandomNumerical<int> random, ProofOfWorkArgs args, ILogger<IProofOfWork> logger) : IProofOfWork
{
    private readonly IRandomNumerical<int> _random = random;
    private readonly ILogger<IProofOfWork> _logger = logger;
    public string? GetHash(in Block? blockToProve)
    {
        if(blockToProve is null) return null;

        using var sha256 = SHA256.Create();
        var blockAsString = new StringBuilder();
        blockAsString.Append(blockToProve.Index)
                        .Append(blockToProve.TimeStamp)
                        .Append(blockToProve.Content)
                        .Append(blockToProve.Proof)
                        .Append(blockToProve.PreviousHash);
        

        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(blockAsString.ToString()));
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