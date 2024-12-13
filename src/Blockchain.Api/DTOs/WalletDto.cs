namespace Blockchain.Api.DTOs;

public class WalletDto(string NickName)
{
    public string NickName { get; init; } = NickName;
}
