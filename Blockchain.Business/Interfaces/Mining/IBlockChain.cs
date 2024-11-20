namespace Blockchain.Business.Interfaces.Mining;
public interface IBlockChain<BlockType> : IEnumerable<BlockType> where BlockType : class
{
    BlockType? LastBlock { get; }
    public BlockType this[int index] { get; }
    public int LastIndex { get; }
    void AddBlock(BlockType newBlock);
    IReadOnlyCollection<BlockType> CheckChain();
}