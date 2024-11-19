namespace Blockchain.Business.ProofOfWork
{
    public readonly struct ProofOfWorkArgs(string difficulty, int nonceMaxValue)
    {
        public string Difficulty { get; init; } = difficulty;
        public int NonceMaxValue { get; init; } = nonceMaxValue;
    }
}