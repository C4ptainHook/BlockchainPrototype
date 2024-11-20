﻿using Blockchain.Business.Models.Block;

namespace Blockchain.Business.Interfaces.PoW;

public interface IProofOfWork
{
    string? GetHash(in Block? blockToProve);
    bool IsHashValid(in string hashToCheck);
    int GetNewNonce();
}