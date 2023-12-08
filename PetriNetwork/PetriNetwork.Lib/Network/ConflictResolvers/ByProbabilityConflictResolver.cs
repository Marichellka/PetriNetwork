using System.Diagnostics;
using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Network.ConflictResolvers;

public class ByProbabilityConflictResolver: IConflictResolver
{
    public Transition ResolveConflict(List<Transition> conflictTransitions, List<Position> positions)
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