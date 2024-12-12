namespace Blockchain.Business.Models;

public record TransactionModel : BaseModel
{
    public string SenderWallet { get; set; }
    public string RecipientWallet { get; set; }
    public decimal Amount { get; init; }
    public DateTime TimeStamp { get; init; }
    public string? BlockId { get; set; }

    public TransactionModel(string senderWallet, string recipientWallet, decimal amount)
    {
        SenderWallet = senderWallet;
        RecipientWallet = recipientWallet;
        Amount = amount;
        TimeStamp = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    }
}
