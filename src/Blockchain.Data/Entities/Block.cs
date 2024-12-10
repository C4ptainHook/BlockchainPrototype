using MongoDB.Bson;

namespace Blockchain.Data.Entities;

public class Block : BaseEntity
{
    public int Index { get; set; }
    public DateTime TimeStamp { get; set; }
    public int Proof { get; set; }
    public string? PreviousHash { get; set; }
    public required string MerkleRoot { get; set; }
}
