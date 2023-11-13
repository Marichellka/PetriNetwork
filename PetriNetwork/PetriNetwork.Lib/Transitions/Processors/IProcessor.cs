namespace PetriNetwork.Lib.Transitions.Processors;

public interface IProcessor
{
    public PriorityQueue<IEnumerable<object>, double> ProcessingItems { get; }
    public double NextEventTime { get; } 

    public void Process(IEnumerable<object> markers, double timeCompletion);

    public IEnumerable<object> EndProcess();
}