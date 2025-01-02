namespace Blockchain.Api.DTOs;

public class WalletDto
{
    public string NickName { get; init; }

    public WalletDto(string nickName)
    {
        NickName = nickName;
    }
}
