namespace simulation_lab_6
{
    public class Operators : IAgent
    {
        private ExponentialRNG _rng;
        private double _serviceTimeParameter;
        private uint _totalCount;
        private uint _currentCount;
        private uint _servedPeopleCount;

        private Queue _customersQueue;

        public Operators(double serviceTimeParameter, uint count, Queue customersQueue)
        {
            _rng = new ExponentialRNG();
            _serviceTimeParameter = serviceTimeParameter;
            _totalCount = count;
            _currentCount = 0;
            _servedPeopleCount = 0;

            _customersQueue = customersQueue;
        }

        public bool HasFreeOperator
        {
            get
            {
                return _currentCount < _totalCount;
            }
        }

        public uint ServedPeopleCount
        {
            get
            {
                return _servedPeopleCount;
            }
        }

        public void Serve()
        {
            if (_currentCount == _totalCount)
            {
                return;
            }

            _currentCount++;
        }

        public double GetNextEventTime()
        {
            if (_currentCount == 0)
            {
                return double.PositiveInfinity;
            }

            return _rng.GetRandomNumber(_serviceTimeParameter * _currentCount);
        }

        public void ProcessEvent()
        {
            _servedPeopleCount++;

            if (_customersQueue.IsEmpty)
            {
                _currentCount--;

                return;
            }

            _customersQueue.Dequeue();
        }
    }
}
