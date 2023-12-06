using PetriNetwork.Lib.Markers;
using PetriNetwork.Lib.Markers.Queues;

namespace PetriNetwork.Lib.Positions.Queues;

public class NodeByRepairTimePrioritySelector: IPrioritySelector<object>
{
    public IComparable GetPriority(object item)
    {
        // less repair time == more priority
        return -(item as Node).RepairTime;
    }
}