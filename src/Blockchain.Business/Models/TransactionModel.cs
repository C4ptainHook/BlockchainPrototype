namespace Blockchain.Business.Models;

public record TransactionModel
{
    public string Id { get; init; }
    public string Sender { get; init; }
    public string Recipient { get; init; }
    public decimal Amount { get; init; }
    public DateTime TimeStamp { get; init; }
    public string? BlockId { get; init; }

    public TransactionModel(string sender, string recipient, decimal amount)
    {
        Sender = sender;
        Recipient = recipient;
        Amount = amount;
        TimeStamp = DateTime.Now;
    }
}
