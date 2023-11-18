using PetriNetwork.Lib.Positions;

namespace PetriNetwork.Lib.Transitions.ConflictResolvers;

public interface IConflictResolver
{
    public abstract Transition ResolveConflict(List<Transition> conflictTransitions,  List<Position> positions);
}