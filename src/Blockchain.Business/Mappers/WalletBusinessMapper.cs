using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace Blockchain.Business.Mappers;

[Mapper]
public partial class WalletBusinessMapper : IMapper<WalletModel, Wallet>
{
    [MapperIgnoreSource(nameof(Wallet.Id))]
    [MapperIgnoreTarget(nameof(Wallet.Id))]
    public partial Wallet Map(WalletModel source);

    public partial WalletModel Map(Wallet source);

    public partial IEnumerable<Wallet> Map(IEnumerable<WalletModel> from);

    public partial IEnumerable<WalletModel> Map(IEnumerable<Wallet> From);
}
