using System;

namespace PinCodeGenerator
{
    internal class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _random;
        private readonly int _minValue;
        private readonly int _maxValue;

        public RandomNumberGenerator(int minValue, int maxValue)
        {
            _random = new Random();
            _minValue = minValue;
            _maxValue = maxValue;
        }
    
        public int Next()
        {
            return _random.Next(_minValue, _maxValue);
        }
    }
}