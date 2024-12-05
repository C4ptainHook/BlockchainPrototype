using MongoDB.Bson;

namespace Blockchain.Data.Entities;

public class Transaction : BaseEntity
{
    public decimal Amount { get; set; }
    public ObjectId RecipientId { get; set; }
    public ObjectId SenderId { get; set; }
    public DateTime TimeStamp { get; set; }
    public Wallet Recipient { get; set; }
    public Wallet Sender { get; set; }
    public ObjectId? BlockId { get; set; }

    public Transaction()
    {
        Recipient ??= default!;
        Sender ??= default!;
    }
}
