namespace Blockchain.Business.Models;

public readonly struct ProofOfWorkServiceArgs(string difficulty, int nonceMaxValue)
{
    public string Difficulty { get; init; } = difficulty;
    public int NonceMaxValue { get; init; } = nonceMaxValue;
}
