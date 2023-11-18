using PetriNetwork.Lib.Positions;

namespace PetriNetwork.Lib.Transitions.ConflictResolvers;

public class ByPriorityConflictResolver: IConflictResolver
{
    public Transition ResolveConflict(List<Transition> conflictTransitions,  List<Position> positions)
    {
        return conflictTransitions.MaxBy(t => t.Priority);
    }

    public List<Transition> GetMaxPriorityTransitions(List<Transition> conflictTransitions)
    {
        double maxPriority = conflictTransitions.Max(t => t.Priority);
        return conflictTransitions.Where(t => t.Priority == maxPriority).ToList();
    }
}