﻿using System.Numerics;

namespace Blockchain.Business.RandomWrappers
{
    public interface IRandomNumerical<T> where T : INumber<T>
    {
        int Next(T minValue, T maxValue);
        int Next(T maxValue);
    }
}