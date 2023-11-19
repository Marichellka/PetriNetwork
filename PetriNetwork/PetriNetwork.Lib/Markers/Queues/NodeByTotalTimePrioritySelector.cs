namespace PetriNetwork.Lib.Markers.Queues;

public class NodeByTotalTimePrioritySelector: IPrioritySelector<object>
{
    public IComparable GetPriority(object item)
    {
        // more time in system == more priority
        return (item as Node).TimeInSystem;
    }
}