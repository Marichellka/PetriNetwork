using PetriNetwork.Lib.Markers;
using PetriNetwork.Lib.Transitions.DelayProviders;

namespace PetriNetwork.Lib.Transitions.Processors;

public class NodeCreationProcessor: Processor
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

    private IDelayProvider _repairTimeProvider;

    public NodeCreationProcessor(IDelayProvider repairTimeProvider)
    {
        _repairTimeProvider = repairTimeProvider;
        ProcessingItems = new PriorityQueue<IEnumerable<object>, double>();
    }   
    
    public override void Process(IEnumerable<object> markers, double delay)
    {
        var node = new Node();
        node.RepairTime = _repairTimeProvider.GetDelay(new List<Node>(){node});
        List<object> newMarkers = markers.ToList();
        newMarkers.Add(node);
        
        ProcessingItems.Enqueue(newMarkers, CurrTime+delay);
    }

    public override IEnumerable<object> EndProcess()
    {
        return ProcessingItems.Dequeue();
    }
}