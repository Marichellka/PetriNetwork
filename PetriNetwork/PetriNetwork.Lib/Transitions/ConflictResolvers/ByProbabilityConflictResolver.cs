using System.Diagnostics;

namespace PetriNetwork.Lib.Transitions.ConflictResolvers;

public class ByProbabilityConflictResolver: IConflictResolver
{
    public Transition ResolveConflict(List<Transition> conflictTransitions)
    {
        if (Math.Abs(conflictTransitions.Sum(t => t.Probability) - 1) > 0.00001)
            throw new ArgumentException("Probabilities are not equal 1");
        
        double random = Random.Shared.NextDouble();
        double prob = 0;

        foreach (var conflictTransition in conflictTransitions)
        {
            if (random < conflictTransition.Probability + prob)
            {
                return conflictTransition;
            }

            prob += conflictTransition.Probability;
        }

        throw new UnreachableException();
    }
    
}