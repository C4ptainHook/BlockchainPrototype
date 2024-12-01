namespace Blockchain.Data.Entities;

public class Block : BaseEntity
{
    public DateTime TimeStamp { get; set; }
    public int Proof { get; set; }
    public string? PreviousHash { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
}
