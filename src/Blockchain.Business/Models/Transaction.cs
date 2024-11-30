namespace Blockchain.Business.Models;

public record Transaction
{
    public string Sender { get; init; }
    public string Recipient { get; init; }
    public decimal Amount { get; init; }

    public Transaction(string sender, string recipient, decimal amount)
    {
        Sender = sender;
        Recipient = recipient;
        Amount = amount;
    }
}
