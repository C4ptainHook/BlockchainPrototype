namespace Blockchain.Api.DTOs;

public partial class TransactionDto
{
    public int Sender { get; set; }
    public int Recipient { get; set; }
    public decimal Amount { get; set; }
}
