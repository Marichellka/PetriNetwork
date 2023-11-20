namespace PetriNetwork.Lib.Markers.Queues;

public class Queue<TItem>: IQueue<TItem>
{
    private System.Collections.Generic.Queue<TItem> _queue;
    public int Count { get=>_queue.Count; }
    private double _mean; 
    public double Mean { get => Entered==0?0:_mean/Entered;}
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
        _mean += Count;
        Entered++;
        _queue.Enqueue(item);
    }
    
    public TItem Dequeue()
    {
        return _queue.Dequeue();
    }
}