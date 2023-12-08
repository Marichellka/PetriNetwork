using PetriNetwork.Lib.Markers;
using PetriNetwork.Lib.Markers.Queues;

namespace PetriNetwork.Lib.Positions.Queues;

public class NodeByTotalTimePrioritySelector: IPrioritySelector<object>
{
    public IComparable GetPriority(object item)
    {
        if ((item as Node).CycleCount>=1) // items for remake
            // more time in system == more priority
            return (item as Node).TimeInSystem;
        
        // new items
        // less repair time == more priority
        return -(item as Node).RepairTime;
    }
}