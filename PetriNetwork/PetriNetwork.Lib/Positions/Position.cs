using PetriNetwork.Lib.Arcs;
using PetriNetwork.Lib.Markers.Queues;
using PetriNetwork.Lib.Network;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Positions;

public class Position : INetworkItem
{
    public string Name { get; }
    public double CurrTime { get; set; } = 0;
    public int MarkersCount => Markers.Count;
    public IQueue<object> Markers { get; }
    public Type MarkersType { get; }
    public List<ArcIn> ArcsIn { get; } = new List<ArcIn>();
    public event Action<object, double> OnEnter;
    public event Action<object, double> OnExit; 

    public Position(string name)
    {
        Name = name;
        Markers = new Markers.Queues.Queue<object>();
        MarkersType = typeof(object);
    }
    
    public Position(string name, IQueue<object> markers, Type type)
    {
        Name = name;
        Markers = markers;
        MarkersType = type;
    }
    
    public Position(string name, IEnumerable<object> markers)
    {
        Name = name;
        Markers = new Markers.Queues.Queue<object>(markers);
        MarkersType = Markers.Peek().GetType();
    }


    public object GetMarker()
    {
        object marker = Markers.Dequeue();
        OnExit?.Invoke(marker, CurrTime);
        return marker;
    }

    public IEnumerable<object> GetMarkers(uint count)
    {
        if (Markers.Count < count)
            throw new ArgumentOutOfRangeException($"Position has less markers ({Markers.Count}) than asked({count})");
        
        List<object> markers = new List<object>();
        for (int i = 0; i < count; i++)
        {
            object marker = Markers.Dequeue();
            OnExit?.Invoke(marker, CurrTime);
            markers.Add(marker);
        }

        return markers;
    }

    public void AddMarker(object marker)
    {
        OnEnter?.Invoke(marker, CurrTime);
        Markers.Enqueue(marker);
    }

    public void AddMarkers(IEnumerable<object> markers)
    {
        foreach (var marker in markers)
        {
            OnEnter?.Invoke(marker, CurrTime);
            Markers.Enqueue(marker);
        }
    }

    public void DebugPrint()
    {
        Console.WriteLine($"{Name}:");
        
    }

    public IEnumerable<Transition> GetOutTransitions()
    {
        HashSet<Transition> transitions = new();
        foreach (var arcIn in ArcsIn)
        {
            transitions.Add(arcIn.Transition);
        }

        return transitions;
    }
}