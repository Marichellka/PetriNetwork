namespace PetriNetwork.Lib.Markers.Queues;

public class Queue<TItem>: IQueue<TItem>
{
    private System.Collections.Generic.Queue<TItem> _queue;
    public int Count { get=>_queue.Count; }

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
        _queue.Enqueue(item);
    }
    
    public TItem Dequeue()
    {
        return _queue.Dequeue();
    }
}