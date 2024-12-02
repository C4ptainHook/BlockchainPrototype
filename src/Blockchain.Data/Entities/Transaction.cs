namespace Blockchain.Data.Entities;

public class Transaction : BaseEntity
{
    public decimal Amount { get; set; }
    public int RecipientId { get; set; }
    public int SenderId { get; set; }
    public Wallet Recipient { get; set; }
    public Wallet Sender { get; set; }

    public Block? Block { get; set; }

    public Transaction(decimal amount, int recipientId, int senderId)
    {
        Amount = amount;
        RecipientId = recipientId;
        SenderId = senderId;
        Recipient = default!;
        Sender = default!;
    }
}
