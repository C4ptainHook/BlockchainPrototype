using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Mining;

public interface IMiner
{
    Task<Block> MineBlockAsync();
}
