namespace PetriNetwork.Lib.Transitions.Processors;

public class ItemChangingProcessor: IProcessor
{
    public PriorityQueue<IEnumerable<object>, double> ProcessingItems { get; }
    private Action<object> _changingAction;

    public double NextEventTime
    {
        get
        {
            if (ProcessingItems.TryPeek(out _, out double priority))
                return priority;
            return Double.MaxValue;
        }
    }

    public ItemChangingProcessor(Action<object> changingAction)
    {
        _changingAction = changingAction;
        ProcessingItems = new PriorityQueue<IEnumerable<object>, double>();
    }   
    
    public void Process(IEnumerable<object> markers, double timeCompletion)
    {
        foreach (var marker in markers)
        {
            _changingAction(marker);
        }
        ProcessingItems.Enqueue(markers, timeCompletion);
    }

    public IEnumerable<object> EndProcess()
    {
        return ProcessingItems.Dequeue();
    }
}