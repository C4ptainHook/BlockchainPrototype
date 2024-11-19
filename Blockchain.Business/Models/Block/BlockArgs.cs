namespace Blockchain.Business.Models.Block;

public readonly struct BlockArgs
{
    public int Index { get; }
    public DateTime TimeStamp { get; }
    public int Proof { get; }
    public string? PreviousHash { get; }

    public BlockArgs(int index, DateTime timeStamp, int proof, string? previousHash)
    {
        Index = index;
        TimeStamp = timeStamp;
        Proof = proof;
        PreviousHash = previousHash;
    }
}
