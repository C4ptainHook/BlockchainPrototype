namespace Blockchain.Business.Models;

public record TransactionModel
{
    public int Sender { get; init; }
    public int Recipient { get; init; }
    public decimal Amount { get; init; }

    public TransactionModel(int sender, int recipient, decimal amount)
    {
        Sender = sender;
        Recipient = recipient;
        Amount = amount;
    }
}
