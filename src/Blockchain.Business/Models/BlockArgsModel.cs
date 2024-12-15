namespace Blockchain.Business.Models;

public readonly struct BlockArgsModel
{
    public int Index { get; }
    public DateTime TimeStamp { get; }
    public int Proof { get; }
    public string? PreviousHash { get; }

    public BlockArgsModel(int index, DateTime timeStamp, int proof, string? previousHash)
    {
        Index = index;
        TimeStamp = timeStamp;
        Proof = proof;
        PreviousHash = previousHash;
    }
}
