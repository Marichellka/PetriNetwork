namespace PetriNetwork.Lib.Transitions.Processors;

public interface IProcessor
{
    public PriorityQueue<IEnumerable<object>, double> ProcessingItems { get; }
    public double NextEventTime { get; }
    double CurrTime { get; set; }

    public void Process(IEnumerable<object> markers, double delay);

    public IEnumerable<object> EndProcess();
}