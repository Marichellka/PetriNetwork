namespace PetriNetwork.Lib.Transitions.ConflictResolvers;

public class ComplexConflictResolver: IConflictResolver
{
    public Transition ResolveConflict(List<Transition> conflictTransitions)
    {
        var maxPriorityTransitions = new ByPriorityConflictResolver().GetMaxPriorityTransitions(conflictTransitions);
        
        var transition = new ByProbabilityConflictResolver().ResolveConflict(maxPriorityTransitions);

        if (transition.Probability < 1)
            return transition;
        
        return new RandomConflictResolver().ResolveConflict(conflictTransitions);
    }
}