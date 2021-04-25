namespace simulation_lab_6
{
    public class Queue
    {
        private uint _count;

        public Queue()
        {
            _count = 0;
        }

        public bool IsEmpty
        {
            get
            {
                return _count == 0;
            }
        }

        public uint Count
        {
            get
            {
                return _count;
            }
        }

        public void Enqueue()
        {
            _count++;
        }

        public void Dequeue()
        {
            _count--;
        }
    }
}
