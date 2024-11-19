namespace Blockchain.Business.Models.Block;

public record Block
{
    public int Index { get; init; }
    public DateTime TimeStamp { get; init; }
    public object Content { get; init; }
    public int Proof { get; init; }
    public string? PreviousHash { get; init; }

    public Block(object content, BlockArgs args)
    {
        Index = args.Index;
        TimeStamp = args.TimeStamp;
        Content = content;
        Proof = args.Proof;
        PreviousHash = args.PreviousHash;
    }
}
