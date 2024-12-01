namespace Blockchain.Data.Entities;

public class Block
{
    public required string HashId { get; set; }
    public DateTime TimeStamp { get; set; }
    public int Proof { get; set; }
    public string? PreviousHash { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
}
