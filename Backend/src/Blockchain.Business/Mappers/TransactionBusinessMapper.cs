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
    [MapProperty(nameof(Transaction.SenderId), nameof(TransactionModel.SenderWallet))]
    [MapProperty(nameof(Transaction.RecipientId), nameof(TransactionModel.RecipientWallet))]
    public partial TransactionModel Map(Transaction transaction);

    [MapperIgnoreTarget(nameof(Transaction.Sender))]
    [MapperIgnoreTarget(nameof(Transaction.Recipient))]
    [MapProperty(nameof(TransactionModel.SenderWallet), nameof(Transaction.SenderId))]
    [MapProperty(nameof(TransactionModel.RecipientWallet), nameof(Transaction.RecipientId))]
    public partial Transaction Map(TransactionModel transactionModel);

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

    public partial IEnumerable<TransactionModel> Map(IEnumerable<Transaction> transactions);

    public partial IEnumerable<Transaction> Map(IEnumerable<TransactionModel> transactionModels);
}
