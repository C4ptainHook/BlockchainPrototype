namespace Blockchain.Business.Models;

public record BlockModel
{
    public int Index { get; init; }
    public DateTime TimeStamp { get; init; }
    public int Proof { get; init; }
    public ICollection<TransactionModel>? Transactions { get; init; }
    public string? PreviousHash { get; init; }

    public BlockModel(
        object content,
        BlockArgs args,
        ICollection<TransactionModel>? transactions = default
    )
    {
        Index = args.Index;
        TimeStamp = args.TimeStamp;
        Proof = args.Proof;
        PreviousHash = args.PreviousHash;
        Transactions = transactions;
    }
}
