namespace Blockchain.Api.DTOs;

public class TransactionDto(string SenderWalletName, string RecipientWalletName, decimal Amount)
{
    public string SenderWalletName { get; init; } = SenderWalletName;
    public string RecipientWalletName { get; init; } = RecipientWalletName;
    public decimal Amount { get; init; } = Amount;
}
