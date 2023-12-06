using PetriNetwork.Lib.Markers;
using PetriNetwork.Lib.Markers.Queues;

namespace PetriNetwork.Lib.Positions.Queues;

public class NodeByTotalTimePrioritySelector: IPrioritySelector<object>
{
    public IComparable GetPriority(object item)
    {
        // more time in system == more priority
        return (item as Node).TimeInSystem;
    }
}