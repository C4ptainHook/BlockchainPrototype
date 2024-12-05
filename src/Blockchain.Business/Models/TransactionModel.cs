namespace Blockchain.Business.Models;

public record TransactionModel
{
    public string Sender { get; init; }
    public string Recipient { get; init; }
    public decimal Amount { get; init; }

    public DateTime TimeStamp { get; init; }

    public TransactionModel(string sender, string recipient, decimal amount)
    {
        Sender = sender;
        Recipient = recipient;
        Amount = amount;
        TimeStamp = DateTime.Now;
    }
}
