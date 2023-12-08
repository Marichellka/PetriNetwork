using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Network.ConflictResolvers;

public interface IConflictResolver
{
    public Transition ResolveConflict(
        List<Transition> conflictTransitions,  List<Position> positions);
}