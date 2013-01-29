using System;

namespace Blackjack
{
    public class Random : IRandom
    {
        private readonly System.Random _random;

        public Random()
        {
            _random = new System.Random(DateTime.Now.Millisecond);
        }

        public int Next(int max)
        {
            return _random.Next(max);
        }
    }
}