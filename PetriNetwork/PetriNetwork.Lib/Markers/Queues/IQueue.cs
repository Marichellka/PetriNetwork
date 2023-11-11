namespace PetriNetwork.Lib.Markers.Queues;

public interface IQueue<T>
{
    public int Count { get; }
    
    public T Dequeue();

    public T Peek();

    public void Enqueue(T item);

}