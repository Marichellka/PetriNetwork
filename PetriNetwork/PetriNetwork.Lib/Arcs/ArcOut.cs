using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Arcs;

public class ArcOut<T> : Arc<T>
{
    public ArcOut(Position<T> position, Transition transition) : base(position, transition)
    { }

    public void SetMarker(T marker)
    {
        Position.AddMarker(marker);
    }
}