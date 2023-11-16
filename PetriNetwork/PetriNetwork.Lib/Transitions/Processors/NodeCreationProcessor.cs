using PetriNetwork.Lib.Markers;
using PetriNetwork.Lib.Transitions.DelayProviders;

namespace PetriNetwork.Lib.Transitions.Processors;

public class NodeCreationProcessor: IProcessor
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

    private IDelayProvider _repairTimeProvider;

    public NodeCreationProcessor(IDelayProvider repairTimeProvider)
    {
        _repairTimeProvider = repairTimeProvider;
        ProcessingItems = new PriorityQueue<IEnumerable<object>, double>();
    }   
    
    public void Process(IEnumerable<object> markers, double timeCompletion)
    {
        foreach (var marker in markers)
        {
            if (marker is Node node)
            {
                node.RepairTime = _repairTimeProvider.GetDelay(new List<Node>(){node});
            }
        }
        
        ProcessingItems.Enqueue(markers, timeCompletion);
    }

    public IEnumerable<object> EndProcess()
    {
        return ProcessingItems.Dequeue();
    }
}