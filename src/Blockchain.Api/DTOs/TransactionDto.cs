namespace Blockchain.Api.DTOs;

public partial class TransactionDto
{
    public string Sender { get; set; }
    public string Recipient { get; set; }
    public decimal Amount { get; set; }
}
