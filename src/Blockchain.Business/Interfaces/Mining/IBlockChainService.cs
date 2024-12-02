namespace Blockchain.Business.Interfaces.Mining;

public interface IBlockchainService<TBlockType> : IEnumerable<TBlockType>
    where TBlockType : class
{
    TBlockType? LastBlock { get; }
    public TBlockType this[int index] { get; }
    public int LastIndex { get; }
    void AddBlock(TBlockType newBlock);
    IReadOnlyCollection<TBlockType> CheckChain();
}
