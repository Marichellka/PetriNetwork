using PetriNetwork.Lib.Positions;

namespace PetriNetwork.Lib.Transitions.ConflictResolvers;

public class ComplexConflictResolver: IConflictResolver
{
    public Transition ResolveConflict(List<Transition> conflictTransitions,  List<Position> positions)
    {
        var maxPriorityTransitions = new ByPriorityConflictResolver().GetMaxPriorityTransitions(conflictTransitions);
        
        if (maxPriorityTransitions[0].Probability < 1)
            return new ByProbabilityConflictResolver().ResolveConflict(maxPriorityTransitions, positions);

        return new RandomConflictResolver().ResolveConflict(conflictTransitions, positions);
    }
}