namespace PetriNetwork.Lib.Transitions.Processors;

public class BasicProcessor: IProcessor
{
    public PriorityQueue<IEnumerable<object>, double> ProcessingItems { get; }
    public double NextEventTime
    {
        get
        {
            if (ProcessingItems.TryPeek(out _, out double priority))
                return priority;
            return Double.MaxValue;
        }
    }

    public double CurrTime { get; set; }

    public BasicProcessor()
    {
        ProcessingItems = new PriorityQueue<IEnumerable<object>, double>();
    }   
    
    public void Process(IEnumerable<object> markers, double delay)
    {
        ProcessingItems.Enqueue(markers, CurrTime+delay);
    }

    public IEnumerable<object> EndProcess()
    {
        return ProcessingItems.Dequeue();
    }
}