using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.PoW;

public interface IProofOfWorkService
{
    string? GetHash(in BlockModel? blockToProve);
    bool IsHashValid(in string hashToCheck);
    bool IsChainValid(in IEnumerable<BlockModel> chainToCheck);
    int GetNewNonce();
}
