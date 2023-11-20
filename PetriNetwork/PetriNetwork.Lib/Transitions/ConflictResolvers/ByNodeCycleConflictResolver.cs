using PetriNetwork.Lib.Markers;
using PetriNetwork.Lib.Positions;

namespace PetriNetwork.Lib.Transitions.ConflictResolvers;

public class ByNodeCycleConflictResolver: IConflictResolver
{
    private Transition _returnTransition;
    private const double ReturnProbability = 0.15;

    public ByNodeCycleConflictResolver(Transition returnTransition)
    {
        _returnTransition = returnTransition;
    }

    public Transition ResolveConflict(List<Transition> conflictTransitions,  List<Position> positions)
    {
        if (conflictTransitions.Contains(_returnTransition))
        {
            var cycles = (positions[0].Markers.Peek() as Node).CycleCount;
            var prob = Math.Pow(ReturnProbability, cycles);
            if (Random.Shared.NextDouble() < prob)
                return _returnTransition;

            conflictTransitions.Remove(_returnTransition);
        }

        return new ComplexConflictResolver().ResolveConflict(conflictTransitions, positions);
    }
}