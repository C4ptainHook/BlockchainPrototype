using Blockchain.Api.DTOs;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Riok.Mapperly.Abstractions;

namespace Blockchain.Api.Mappers;

[Mapper]
public partial class WalletApiMapper : IMapper<WalletDto, WalletModel>
{
    [MapperIgnoreTarget(nameof(WalletModel.Id))]
    [MapperIgnoreTarget(nameof(WalletModel.Balance))]
    public partial WalletModel Map(WalletDto from);

    [MapperIgnoreSource(nameof(WalletModel.Id))]
    [MapperIgnoreSource(nameof(WalletModel.Balance))]
    public partial WalletDto Map(WalletModel to);

    public partial IEnumerable<WalletModel> Map(IEnumerable<WalletDto> from);

    public partial IEnumerable<WalletDto> Map(IEnumerable<WalletModel> to);
}
