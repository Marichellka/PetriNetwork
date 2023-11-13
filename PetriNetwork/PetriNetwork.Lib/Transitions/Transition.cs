using PetriNetwork.Lib.Arcs;
using PetriNetwork.Lib.Markers.Filters;
using PetriNetwork.Lib.Transitions.DelayProviders;
using PetriNetwork.Lib.Transitions.Processors;

namespace PetriNetwork.Lib.Transitions;

public class Transition
{
    public List<ArcIn<object>> ArcsIn { get; }
    public Dictionary<ArcOut<object>, IMarkerFilter> ArcsOut { get; }
    public IDelayProvider DelayProvider { get; }
    public IProcessor Processor { get; }
    public int Priority { get; }
    public double CurrTime { set; get; }

    public double NextEventTime => Processor.NextEventTime;
    
    
    public Transition(
        List<ArcIn<object>> arcsIn, Dictionary<ArcOut<object>, IMarkerFilter> arcsOut, 
        IDelayProvider delayProvider, IProcessor? processor=null,  int priority=0)
    {
        CurrTime = 0;
        DelayProvider = delayProvider;
        Priority = priority;
        ArcsIn = arcsIn;
        ArcsOut = arcsOut;
        Processor = processor ?? new BasicProcessor();
    }

    public bool IsConditionsDone()
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
}