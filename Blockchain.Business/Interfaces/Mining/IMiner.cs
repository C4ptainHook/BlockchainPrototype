using Blockchain.Business.Models.Block;

namespace Blockchain.Business.Interfaces.Mining;

public interface IMiner
{
    Task<Block> MineBlockAsync();
}
