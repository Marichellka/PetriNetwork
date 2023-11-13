using PetriNetwork.Lib.Position;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Arcs;

public class ArcIn<T>: Arc<T>
{

    public ArcIn(Position<T> position, Transition transition) : base(position, transition)
    {
    }
    
    
    public T GetMarker()
    {
        return Position.GetMarker();
    }

    public bool IsReady()
    {
        return Position.MarkersCount >= 1;
    }
}
