using PetriNetwork.Lib.Markers.Queues;

namespace PetriNetwork.Lib.Positions;

public class Position<T>
{
    public string Name { get; }
    public int MarkersCount => Markers.Count;
    public IQueue<T> Markers { get; }
    
    public Position(string name)
    {
        Name = name;
        Markers = new Markers.Queues.Queue<T>();
    }
    
    public Position(string name, IQueue<T> markers)
    {
        Name = name;
        Markers = markers;
    }


    public T GetMarker()
    {
        return Markers.Dequeue();
    }

    public IEnumerable<T> GetMarkers(uint count)
    {
        if (Markers.Count < count)
            throw new ArgumentOutOfRangeException($"Position has less markers ({Markers.Count}) than asked({count})");
        
        List<T> markers = new List<T>();
        for (int i = 0; i < count; i++)
        {
            markers.Add(Markers.Dequeue());
        }

        return markers;
    }

    public void AddMarker(T marker)
    {
        Markers.Enqueue(marker);
    }

    public void AddMarkers(IEnumerable<T> markers)
    {
        foreach (var marker in markers)
        {
            Markers.Enqueue(marker);
        }
    }
}