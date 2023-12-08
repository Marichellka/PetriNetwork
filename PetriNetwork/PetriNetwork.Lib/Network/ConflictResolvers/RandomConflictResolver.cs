using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Network.ConflictResolvers;

public class RandomConflictResolver: IConflictResolver
{
    public Transition ResolveConflict(List<Transition> conflictTransitions, List<Position> positions)
    {
        return conflictTransitions[Random.Shared.Next(conflictTransitions.Count)];
    }
}