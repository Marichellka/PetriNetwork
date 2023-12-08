using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;

namespace PetriNetwork.Lib.Network.ConflictResolvers;

public class ConflictFinder
{
    public HashSet<Transition> Transitions { get; } = new HashSet<Transition>();
    public HashSet<Position> Positions { get; } = new HashSet<Position>();

    public void TryFindConflict(Transition transition)
    {
        if (Transitions.Contains(transition) || !transition.IsReady())
            return;

        Transitions.Add(transition);
        foreach (var position in transition.GetInPositions())
        {
            if (!Positions.Contains(position))
            {
                Positions.Add(position);
                foreach (var transition1 in position.GetOutTransitions())
                {
                    TryFindConflict(transition1);
                }
            }
        }
    }
}