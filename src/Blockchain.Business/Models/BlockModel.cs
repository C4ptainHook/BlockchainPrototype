namespace Blockchain.Business.Models;

public record BlockModel
{
    public int Index { get; init; }
    public DateTime TimeStamp { get; init; }
    public int Proof { get; init; }
    public ICollection<string>? TransactionIds { get; init; }
    public string? PreviousHash { get; init; }

    public BlockModel() { }

    public BlockModel(object content, BlockArgs args, ICollection<string>? transactionIds = default)
    {
        Index = args.Index;
        TimeStamp = args.TimeStamp;
        Proof = args.Proof;
        PreviousHash = args.PreviousHash;
        TransactionIds = transactionIds;
    }
}
