namespace PetriNetwork.Lib.Markers.Queues;

public class NodePrioritySelector: IPrioritySelector<object>
{
    public IComparable GetPriority(object item)
    {
        return (item as Node).TotalWaitingTime;
    }
}