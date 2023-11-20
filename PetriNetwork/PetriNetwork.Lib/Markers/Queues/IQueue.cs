namespace PetriNetwork.Lib.Markers.Queues;

public interface IQueue<T>
{
    public int Count { get; }
    public double Mean { get; }
    public int Entered { get; }

    public T Dequeue();

    public T Peek();

    public void Enqueue(T item);
    
    public virtual void DebugPrint()
    {
        Console.WriteLine($"Current size: {Count}");
        Console.WriteLine($"Mean size: {Mean}");
    }
}