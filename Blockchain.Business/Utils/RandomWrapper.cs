using Blockchain.Business.Interfaces;

namespace Blockchain.Business.RandomWrappers
{
    public class RandomWrapper : IRandomNumerical<int>
    {
        private readonly Random _random = new();
        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }
    }
}
