using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Mining;

public interface IBlockService<TBlockType>
    where TBlockType : class
{
    Task<TBlockType?> GetLastBlockAsync();
    Task<TBlockType> AddBlockAsync(TBlockType newBlock);
    Task<int> GetChainLength();
    Task<IEnumerable<TBlockType>> GetFullChainAsync();
    Task RemoveBlockAsync(TBlockType blockToRemove);
}
