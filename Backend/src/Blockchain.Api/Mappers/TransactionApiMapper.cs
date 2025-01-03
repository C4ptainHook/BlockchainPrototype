using Blockchain.Api.DTOs;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Riok.Mapperly.Abstractions;

namespace Blockchain.Api.Mappers;

[Mapper]
public partial class TransactionApiMapper : IMapper<TransactionDto, TransactionModel>
{
    [MapperIgnoreTarget(nameof(TransactionModel.Id))]
    [MapperIgnoreTarget(nameof(TransactionModel.BlockId))]
    [MapperIgnoreTarget(nameof(TransactionModel.TimeStamp))]
    [MapProperty(nameof(TransactionDto.SenderWalletName), nameof(TransactionModel.SenderWallet))]
    [MapProperty(
        nameof(TransactionDto.RecipientWalletName),
        nameof(TransactionModel.RecipientWallet)
    )]
    public partial TransactionModel Map(TransactionDto from);

    [MapperIgnoreSource(nameof(TransactionModel.Id))]
    [MapperIgnoreSource(nameof(TransactionModel.BlockId))]
    [MapperIgnoreSource(nameof(TransactionModel.TimeStamp))]
    [MapProperty(nameof(TransactionModel.SenderWallet), nameof(TransactionDto.SenderWalletName))]
    [MapProperty(
        nameof(TransactionModel.RecipientWallet),
        nameof(TransactionDto.RecipientWalletName)
    )]
    public partial TransactionDto Map(TransactionModel to);

    public partial IEnumerable<TransactionModel> Map(IEnumerable<TransactionDto> from);

    public partial IEnumerable<TransactionDto> Map(IEnumerable<TransactionModel> to);
}
