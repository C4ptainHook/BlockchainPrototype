namespace Blockchain.Business.Transactions;

public record Transaction
{
    public string Sender { get; init; }
    public string Recipient { get; init; }
    public int Amount { get; init; }
    public Transaction(string sender, string recipient, int amount)
    {
        Sender = sender;
        Recipient = recipient;
        Amount = amount;
    }
}
