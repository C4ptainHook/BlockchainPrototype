using System.Collections;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Models;

namespace Blockchain.Business.Services;

public class BlockchainService : IBlockchainService<BlockModel>
{
    private readonly List<BlockModel> _chain = [];

    public int LastIndex => _chain.Count - 1;
    public BlockModel? LastBlock
    {
        get => _chain?.LastOrDefault()!;
    }

    public BlockModel this[int index]
    {
        get { return _chain[index]; }
    }

    public void AddBlock(BlockModel newBlock)
    {
        _chain.Add(newBlock);
    }

    public IReadOnlyCollection<BlockModel> CheckChain()
    {
        return _chain.AsReadOnly<BlockModel>();
    }

    public IEnumerator<BlockModel> GetEnumerator()
    {
        return _chain.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
