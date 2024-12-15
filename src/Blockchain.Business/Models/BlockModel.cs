using System.Diagnostics.CodeAnalysis;

namespace Blockchain.Business.Models;

public record BlockModel : BaseModel
{
    public int Index { get; set; }
    public DateTime TimeStamp { get; set; }
    public int Proof { get; set; }
    public required string MerkleRoot { get; init; }
    public string? PreviousHash { get; set; }

    public BlockModel() { }

    [SetsRequiredMembers]
    public BlockModel(BlockArgsModel args, string merkleRoot)
    {
        Index = args.Index;
        TimeStamp = args.TimeStamp;
        Proof = args.Proof;
        PreviousHash = args.PreviousHash;
        MerkleRoot = merkleRoot;
    }
}
