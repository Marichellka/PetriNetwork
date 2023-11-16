using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Arcs;

public abstract class Arc<T>
{
    public Position<T> Position { get; }
    public Transition Transition { get; }
    
    protected Arc(Position<T> position, Transition transition)
    {
        Position = position;
        Transition = transition;
    }
}