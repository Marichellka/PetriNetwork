namespace PetriNetwork.Lib.Markers.Queues;

public interface IPrioritySelector<T>
{
    public IComparable GetPriority(T item);
}