using System.Runtime.Serialization;
using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using MongoDB.Bson;
using Riok.Mapperly.Abstractions;

namespace Blockchain.Business.Mappers;

[Mapper]
public partial class TransactionBusinessMapper : IMapper<TransactionModel, Transaction>
{
    [MapperIgnoreSource(nameof(Transaction.Sender))]
    [MapperIgnoreSource(nameof(Transaction.Recipient))]
    [MapProperty(nameof(Transaction.SenderId), nameof(TransactionModel.Sender))]
    [MapProperty(nameof(Transaction.RecipientId), nameof(TransactionModel.Recipient))]
    public partial TransactionModel Map(Transaction transaction);

    [MapperIgnoreTarget(nameof(Transaction.Id))]
    [MapperIgnoreSource(nameof(TransactionModel.Id))]
    [MapperIgnoreTarget(nameof(Transaction.Sender))]
    [MapperIgnoreTarget(nameof(Transaction.Recipient))]
    [MapProperty(nameof(TransactionModel.Sender), nameof(Transaction.SenderId))]
    [MapProperty(nameof(TransactionModel.Recipient), nameof(Transaction.RecipientId))]
    public partial Transaction Map(TransactionModel transactionModel);

    [UserMapping]
    private ObjectId IdToObjectId(string id) => new ObjectId(id);

    [UserMapping]
    private string ObjectIdToId(ObjectId id) => id.ToString();

    public partial IEnumerable<TransactionModel> Map(IEnumerable<Transaction> transactions);

    public partial IEnumerable<Transaction> Map(IEnumerable<TransactionModel> transactionModels);
}
