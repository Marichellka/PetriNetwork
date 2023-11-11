using PetriNetwork.Lib.Arcs;
using PetriNetwork.Lib.Transitions.DelayProviders;

namespace PetriNetwork.Lib.Transitions;

public class Transition<T>
{
    public IDelayProvider<T> DelayProvider { get; }
    public int Priority { get; }
    public List<Arc<T>> ArcsIn { get; }
    public List<Arc<T>> ArcsOut { get; }

    public bool IsConditionsDone()
    {
        foreach (var arcIn in ArcsIn)
        {
            if (arcIn.ArcCount > arcIn.Position.MarkersCount)
            {
                return false;
            }
        }

        return true;
    }

    public void StartTransition()
    {
        foreach (var arcIn in ArcsIn)
        {
            arcIn.Position.GetMarkers(arcIn.ArcCount);
        }
    }
    
    // TODO: redo out markers creation
    public void EndTransition()
    {
        foreach (var arcOut in ArcsOut)
        {
            List<T> markers = new List<T>();
            for (int i = 0; i < arcOut.ArcCount; i++)
            {
                markers.Add(default);
            }
            arcOut.Position.AddMarkers(markers);
        }
    }
}