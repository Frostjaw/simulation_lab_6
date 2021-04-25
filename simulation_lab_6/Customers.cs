namespace simulation_lab_6
{
    public class Customers : IAgent
    {
        private ExponentialRNG _rng;
        private double _arrivalTimeParameter;

        private Bank _bank;

        public Customers(double arrivalTimeParameter, Bank bank)
        {
            _rng = new ExponentialRNG();
            _arrivalTimeParameter = arrivalTimeParameter;

            _bank = bank;
        }

        public double GetNextEventTime()
        {
            return _rng.GetRandomNumber(_arrivalTimeParameter);
        }

        public void ProcessEvent()
        {
            _bank.Serve();
        }
    }
}
