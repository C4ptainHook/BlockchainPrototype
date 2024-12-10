namespace Blockchain.Business.Models;

public record TransactionModel : BaseModel
{
    public string Sender { get; init; }
    public string Recipient { get; init; }
    public decimal Amount { get; init; }
    public DateTime TimeStamp { get; init; }
    public string? BlockId { get; set; }

    public TransactionModel(string sender, string recipient, decimal amount)
    {
        Sender = sender;
        Recipient = recipient;
        Amount = amount;
        TimeStamp = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    }
}
