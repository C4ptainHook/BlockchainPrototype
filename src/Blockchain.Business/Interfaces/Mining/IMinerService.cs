using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Mining;

public interface IMinerService
{
    Task<Block> MineBlockAsync();
}