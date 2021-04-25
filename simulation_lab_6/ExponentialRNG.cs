using System;

namespace simulation_lab_6
{
    public class ExponentialRNG
    {
        private readonly Random _rng;

        public ExponentialRNG()
        {
            _rng = new Random();
        }

        public double GetRandomNumber(double lambda)
        {
            return -(Math.Log(_rng.NextDouble()) / lambda);
        }
    }
}
