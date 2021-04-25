namespace simulation_lab_6
{
    public interface IAgent
    {
        double GetNextEventTime();

        void ProcessEvent();
    }
}
