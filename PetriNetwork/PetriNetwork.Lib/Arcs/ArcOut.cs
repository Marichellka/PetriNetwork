using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Arcs;

public class ArcOut : Arc
{
    public ArcOut(Position position, Transition transition) : base(position, transition)
    { }

    public void SetMarker(object marker)
    {
        Position.AddMarker(marker);
    }
}