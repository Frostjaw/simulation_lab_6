namespace simulation_lab_6
{
    public class Bank : IAgent
    {
        private Operators _operators;
        private Queue _customersQueue;

        public Bank(double serviceTimeParameter, uint operatorsCount)
        {
            _customersQueue = new Queue();
            _operators = new Operators(serviceTimeParameter, operatorsCount, _customersQueue);
        }

        public uint QueueLength
        {
            get
            {
                return _customersQueue.Count;
            }
        }

        public uint ServedPeopleCount
        {
            get
            {
                return _operators.ServedPeopleCount;
            }
        }

        public double GetNextEventTime()
        {
            return _operators.GetNextEventTime();
        }

        public void ProcessEvent()
        {
            _operators.ProcessEvent();
        }

        public void Serve()
        {
            if (_operators.HasFreeOperator)
            {
                _operators.Serve();

                return;
            }

            _customersQueue.Enqueue();
        }
    }
}
