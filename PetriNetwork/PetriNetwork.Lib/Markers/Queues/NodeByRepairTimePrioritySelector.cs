namespace PetriNetwork.Lib.Markers.Queues;

public class NodeByRepairTimePrioritySelector: IPrioritySelector<object>
{
    public IComparable GetPriority(object item)
    {
        // less repair time == more priority
        return -(item as Node).RepairTime;
    }
}