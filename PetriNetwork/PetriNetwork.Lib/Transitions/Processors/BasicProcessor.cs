namespace PetriNetwork.Lib.Transitions.Processors;

public class BasicProcessor: Processor
{
    public override PriorityQueue<IEnumerable<object>, double> ProcessingItems { get; }
    public override double NextEventTime
    {
        get
        {
            if (ProcessingItems.TryPeek(out _, out double priority))
                return priority;
            return Double.MaxValue;
        }
    }

    public override double CurrTime { get; set; }

    public BasicProcessor()
    {
        ProcessingItems = new PriorityQueue<IEnumerable<object>, double>();
    }   
    
    public BasicProcessor(PriorityQueue<IEnumerable<object>, double> items)
    {
        ProcessingItems = items;
    }
    
    public override void Process(IEnumerable<object> markers, double delay)
    {
        ProcessingItems.Enqueue(markers, CurrTime+delay);
    }

    public override IEnumerable<object> EndProcess()
    {
        return ProcessingItems.Dequeue();
    }
}