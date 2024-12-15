using System.Diagnostics.CodeAnalysis;

namespace Blockchain.Business.Models;

public class BlockEqualityComparer : IEqualityComparer<BlockModel>
{
    public bool Equals(BlockModel? x, BlockModel? y)
    {
        if (x is null || y is null)
            return false;
        return x.Id == y.Id;
    }

    public int GetHashCode([DisallowNull] BlockModel obj)
    {
        return obj.Id.GetHashCode();
    }
}
