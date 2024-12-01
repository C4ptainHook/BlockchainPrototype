namespace Blockchain.Data.Entities;

public class Transaction : BaseEntity
{
    public decimal Amount { get; set; }
    public Wallet Recepient { get; set; }
    public Wallet Sender { get; set; }

    public Block? Block { get; set; }

    public Transaction(decimal amount, Wallet recepient, Wallet sender)
    {
        Amount = amount;
        Recepient = recepient;
        Sender = sender;
    }
}
