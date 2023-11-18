using PetriNetwork.Lib.Positions;

namespace PetriNetwork.Lib.Transitions.ConflictResolvers;

public class RandomConflictResolver: IConflictResolver
{
    public Transition ResolveConflict(List<Transition> conflictTransitions, List<Position> positions)
    {
        return conflictTransitions[Random.Shared.Next(conflictTransitions.Count)];
    }
}