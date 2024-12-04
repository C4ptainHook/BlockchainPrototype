using System.Runtime.Serialization;
using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace Blockchain.Business.Mappers;

[Mapper]
public partial class TransactionBusinessMapper : IMapper<TransactionModel, Transaction>
{
    [MapperIgnoreSource(nameof(Transaction.Id))]
    [MapperIgnoreSource(nameof(Transaction.Block))]
    [MapperIgnoreSource(nameof(Transaction.Sender))]
    [MapperIgnoreSource(nameof(Transaction.Recipient))]
    [MapProperty(nameof(Transaction.SenderId), nameof(TransactionModel.Sender))]
    [MapProperty(nameof(Transaction.RecipientId), nameof(TransactionModel.Recipient))]
    public partial TransactionModel Map(Transaction transaction);

    [MapperIgnoreTarget(nameof(Transaction.Id))]
    [MapperIgnoreTarget(nameof(Transaction.Block))]
    [MapperIgnoreTarget(nameof(Transaction.Sender))]
    [MapperIgnoreTarget(nameof(Transaction.Recipient))]
    [MapProperty(nameof(TransactionModel.Sender), nameof(Transaction.SenderId))]
    [MapProperty(nameof(TransactionModel.Recipient), nameof(Transaction.RecipientId))]
    public partial Transaction Map(TransactionModel transactionModel);

    public partial IEnumerable<TransactionModel> Map(IEnumerable<Transaction> transactions);

    public partial IEnumerable<Transaction> Map(IEnumerable<TransactionModel> transactionModels);
}
