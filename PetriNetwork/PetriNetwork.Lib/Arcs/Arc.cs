using PetriNetwork.Lib.Position;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Arcs;

public class Arc<T>
{
    public Position<T> Position { get; }
    public Transition<T> Transition { get; }
    public uint ArcCount { get; }
}