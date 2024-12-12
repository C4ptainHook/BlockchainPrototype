namespace Blockchain.Api.DTOs;

public class TransactionDto
{
    public string SenderWalletName { get; set; }
    public string RecipientWalletName { get; set; }
    public decimal Amount { get; set; }
}
