namespace Blockchain.Data.Entities;

public class Wallet : BaseEntity
{
    public required string NickName { get; set; }
    public decimal Balance { get; set; }
}
