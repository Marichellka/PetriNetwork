using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Arcs;

public abstract class Arc
{
    public Position Position { get; }
    public Transition Transition { get; }
    
    protected Arc(Position position, Transition transition)
    {
        Position = position;
        Transition = transition;
    }
}