using Blockchain.Business.CryptoChain;

namespace Blockchain.Business.Mining
{
    public interface IMiner
    {
        Block MineBlock();
    }
}