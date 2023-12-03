
namespace PetriNetwork.Lib.Markers.Queues;

public class PriorityQueue<TItem>: IQueue<TItem>
{
    private PriorityQueue<TItem, IComparable> _priorityQueue;
    private IPrioritySelector<TItem> _prioritySelector;
    public int Count { get=>_priorityQueue.Count; }
    private double _mean;
    private double _updateCount;
    public double Mean { get => _updateCount==0?0:_mean/_updateCount;}
    public int Entered { get; private set; }

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
        Entered++;
        _priorityQueue.Enqueue(item, _prioritySelector.GetPriority(item)); 
    }

    public IEnumerable<TItem> GetEnumerable()
    {
        while (_priorityQueue.TryDequeue(out TItem element, out IComparable _))
        {
            yield return element;
        }
    }

    public TItem Dequeue()
    {
        return _priorityQueue.Dequeue();
    }
    
    public void Update()
    {
        _updateCount++;
        _mean += Count;
    }
}