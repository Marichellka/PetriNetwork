using PetriNetwork.Lib.Arcs;
using PetriNetwork.Lib.Markers.Filters;
using PetriNetwork.Lib.Network;
using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions.DelayProviders;
using PetriNetwork.Lib.Transitions.Processors;

namespace PetriNetwork.Lib.Transitions;

public class Transition: INetworkItem
{
    public string Name { get; }
    public List<ArcIn> ArcsIn { get; }
    public Dictionary<ArcOut, IMarkerFilter> ArcsOut { get; }
    public IDelayProvider DelayProvider { get; }
    public IProcessor Processor { get; }
    public int CountProcessing => Processor.ProcessingItems.Count;
    public double Priority { get; }
    public double Probability { get; }
    public double CurrTime { set; get; }
    public double NextEventTime => Processor.NextEventTime;

    public Transition(
        string name,  IDelayProvider delayProvider, List<ArcIn>? arcsIn=null, 
        Dictionary<ArcOut, IMarkerFilter>? arcsOut=null, 
        IProcessor? processor=null,  int priority=0, double probability=1)
    {
        CurrTime = 0;
        DelayProvider = delayProvider;
        Name = name;
        Probability = probability;
        Priority = priority;
        ArcsIn = arcsIn ?? new List<ArcIn>();
        ArcsOut = arcsOut ?? new Dictionary<ArcOut, IMarkerFilter>();
        Processor = processor ?? new BasicProcessor();
    }

    public bool IsReady()
    {
        foreach (var arcIn in ArcsIn)
        {
            if (!arcIn.IsReady())
            {
                return false;
            }
        }

        return true;
    }

    public void StartTransition()
    {
        List<object> allMarkers = new List<object>();
        foreach (var arcIn in ArcsIn)
        {
            allMarkers.Add(arcIn.GetMarker());
        }
        Processor.Process(allMarkers, CurrTime+DelayProvider.GetDelay(allMarkers));
    }
    
    public void EndTransition()
    {
        var allMarkers = Processor.EndProcess();
        foreach (var (arcOut, filter) in ArcsOut)
        {
            arcOut.SetMarker(filter.Filter(allMarkers));
        }
    }

    public void Check()
    {
        if (ArcsIn.Count == 0)
            throw new Exception($"Transition {Name} has no input positions");

        if (ArcsOut.Count == 0)
            throw new Exception($"Transition {Name} has no output positions");
    }
    
    public void DebugPrint()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Position> GetInPositions()
    {
        HashSet<Position> positions = new();
        foreach (var arcIn in ArcsIn)
        {
            positions.Add(arcIn.Position);
        }

        return positions;
    }
}