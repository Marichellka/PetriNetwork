namespace PetriNetwork.Lib.Transitions.Processors;

public class ItemChangingProcessor: Processor
{
    public override PriorityQueue<IEnumerable<object>, double> ProcessingItems { get; }
    private Action<object> _changingAction;

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

    public ItemChangingProcessor(Action<object> changingAction)
    {
        _changingAction = changingAction;
        ProcessingItems = new PriorityQueue<IEnumerable<object>, double>();
    }   
    
    public override void Process(IEnumerable<object> markers, double delay)
    {
        foreach (var marker in markers)
        {
            _changingAction(marker);
        }
        ProcessingItems.Enqueue(markers, CurrTime+delay);
    }

    public override IEnumerable<object> EndProcess()
    {
        return ProcessingItems.Dequeue();
    }
}