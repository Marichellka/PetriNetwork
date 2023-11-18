using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Arcs;

public class ArcIn: Arc
{

    public ArcIn(Position position, Transition transition) : base(position, transition)
    {
    }
    
    
    public object GetMarker()
    {
        return Position.GetMarker();
    }

    public bool IsReady()
    {
        return Position.MarkersCount >= 1;
    }
}
