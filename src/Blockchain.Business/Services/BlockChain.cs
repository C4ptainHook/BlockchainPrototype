using System.Collections;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Models;

namespace Blockchain.Business.Services;

public class BlockChain : IBlockChain<Block>
{
    private readonly List<Block> _chain = [];

    public int LastIndex => _chain.Count - 1;
    public Block? LastBlock
    {
        get => _chain?.LastOrDefault()!;
    }

    public Block this[int index]
    {
        get { return _chain[index]; }
    }

    public void AddBlock(Block newBlock)
    {
        _chain.Add(newBlock);
    }

    public IReadOnlyCollection<Block> CheckChain()
    {
        return _chain.AsReadOnly<Block>();
    }

    public IEnumerator<Block> GetEnumerator()
    {
        return _chain.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
