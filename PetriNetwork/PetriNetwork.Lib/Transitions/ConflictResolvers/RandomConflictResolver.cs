namespace PetriNetwork.Lib.Transitions.ConflictResolvers;

public class RandomConflictResolver: IConflictResolver
{
    public Transition ResolveConflict(List<Transition> conflictTransitions)
    {
        return conflictTransitions[Random.Shared.Next(conflictTransitions.Count)];
    }
}