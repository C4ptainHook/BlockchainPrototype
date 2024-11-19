using Blockchain.Business.CryptoChain;

namespace Blockchain.Business.ProofOfWork;

public interface IProofOfWork
{
    string? GetHash(in Block? blockToProve);
    bool IsHashValid(in string hashToCheck);
    int GetNewNonce();
}
