using System.Collections;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Models;
using Blockchain.Business.Models.Block;

namespace Blockchain.Business.Services;

public class BlockChain : IBlockChain<Block>
{
    private readonly List<Block> _chain = [];
    private readonly List<object> _currentTransactions = [];

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

    public int RegisterValue(object value)
    {
        var transaction = value as Transaction;
        if (transaction is null)
        {
            throw new ArgumentException($"The {nameof(BlockChain)} supports only transactions.");
        }
        _currentTransactions.Add(value);
        return _chain.Count;
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
