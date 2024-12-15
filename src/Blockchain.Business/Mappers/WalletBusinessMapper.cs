using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using MongoDB.Bson;
using Riok.Mapperly.Abstractions;

namespace Blockchain.Business.Mappers;

[Mapper]
public partial class WalletBusinessMapper : IMapper<WalletModel, Wallet>
{
    public partial Wallet Map(WalletModel source);

    public partial WalletModel Map(Wallet source);

    [UserMapping]
    private ObjectId IdToObjectId(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return ObjectId.Empty;
        }
        return new ObjectId(id);
    }

    [UserMapping]
    private string ObjectIdToId(ObjectId id) => id.ToString();

    public partial IEnumerable<Wallet> Map(IEnumerable<WalletModel> from);

    public partial IEnumerable<WalletModel> Map(IEnumerable<Wallet> From);
}
