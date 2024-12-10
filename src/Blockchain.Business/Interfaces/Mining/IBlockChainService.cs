using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Mining;

public interface IBlockchainService<TBlockType>
    where TBlockType : class
{
    Task<TBlockType?> GetLastBlockAsync();
    Task<TBlockType> AddBlockAsync(TBlockType newBlock);
    Task<int> GetChainLength();
    Task<IEnumerable<TBlockType>> GetFullChainAsync();
}
