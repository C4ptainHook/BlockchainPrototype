namespace Blockchain.Business.Models.Block;

public record Block
{
    public int Index { get; init; }
    public DateTime TimeStamp { get; init; }
    public int Proof { get; init; }
    public ICollection<Transaction>? Transactions { get; init; }
    public string? PreviousHash { get; init; }

    public Block(object content, BlockArgs args, ICollection<Transaction>? transactions = default)
    {
        Index = args.Index;
        TimeStamp = args.TimeStamp;
        Proof = args.Proof;
        PreviousHash = args.PreviousHash;
        Transactions = transactions;
    }
}
