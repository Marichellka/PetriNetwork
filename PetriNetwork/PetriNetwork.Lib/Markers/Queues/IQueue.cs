namespace PetriNetwork.Lib.Markers.Queues;

public interface IQueue<T>
{
    public int Count { get; }
    public double Mean { get; }
    public int Entered { get; }

    public T Dequeue();

    public T Peek();

    public void Enqueue(T item);

    public IEnumerable<T> GetEnumerable();

    public virtual void DebugPrint()
    {
        Console.WriteLine($"Current size: {Count}");
        Console.WriteLine($"Entered: {Entered}");
        Console.WriteLine($"Mean size: {Mean}");
    }

    public void Update();
}