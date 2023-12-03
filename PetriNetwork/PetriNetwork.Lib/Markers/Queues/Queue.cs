namespace PetriNetwork.Lib.Markers.Queues;

public class Queue<TItem>: IQueue<TItem>
{
    private System.Collections.Generic.Queue<TItem> _queue;
    public int Count { get=>_queue.Count; }
    private double _mean;
    private double _updateCount;
    public double Mean { get => _updateCount==0?0:_mean/_updateCount;}
    public int Entered { get; private set; }

    public Queue()
    {
        _queue = new();
    }

    public Queue(IEnumerable<TItem> items)
    {
        _queue = new(items);
    }

    public TItem Peek()
    {
        return _queue.Peek();
    }

    public void Enqueue(TItem item)
    {
        Entered++;
        _queue.Enqueue(item);
    }

    public IEnumerable<TItem> GetEnumerable()
    {
        return _queue;
    }

    public TItem Dequeue()
    {
        return _queue.Dequeue();
    }

    public void Update()
    {
        _updateCount++;
        _mean += Count;
    }
}