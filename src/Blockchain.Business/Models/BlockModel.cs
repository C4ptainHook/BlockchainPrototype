namespace Blockchain.Business.Models;

public record BlockModel : BaseModel
{
    public int Index { get; init; }
    public DateTime TimeStamp { get; init; }
    public int Proof { get; init; }
    public string MerkleRoot { get; init; }
    public string? PreviousHash { get; init; }

    public BlockModel() { }

    public BlockModel(BlockArgsModel args, string merkleRoot)
    {
        Index = args.Index;
        TimeStamp = args.TimeStamp;
        Proof = args.Proof;
        PreviousHash = args.PreviousHash;
        MerkleRoot = merkleRoot;
    }
}
