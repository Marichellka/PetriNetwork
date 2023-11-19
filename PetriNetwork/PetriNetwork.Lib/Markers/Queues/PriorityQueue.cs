
namespace PetriNetwork.Lib.Markers.Queues;

public class PriorityQueue<TItem>: IQueue<TItem>
{
    private PriorityQueue<TItem, IComparable> _priorityQueue;
    private IPrioritySelector<TItem> _prioritySelector;
    public int Count { get=>_priorityQueue.Count; }

    public PriorityQueue(IPrioritySelector<TItem> prioritySelector)
    {
        _prioritySelector = prioritySelector;
        _priorityQueue = new();
    }

    public TItem Peek()
    {
        return _priorityQueue.Peek();
    }

    public void Enqueue(TItem item)
    {
        _priorityQueue.Enqueue(item, _prioritySelector.GetPriority(item)); 
    }
    
    public TItem Dequeue()
    {
        return _priorityQueue.Dequeue();
    }
}