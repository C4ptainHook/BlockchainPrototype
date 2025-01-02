using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Models;

namespace Blockchain.Business.Caching;

public class BlockCachingService
{
    public int Length { get; set; } = -1;
    public ICollection<BlockModel> Blocks = [];
    public readonly ICollection<string> Nodes = [];

    public async Task<Tuple<IEnumerable<BlockModel>, int>> GetLocalChainAsync()
    {
        return await Task.Run(() => new Tuple<IEnumerable<BlockModel>, int>(Blocks, Length));
    }
}
