using PetriNetwork.Lib.Arcs;
using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;
using PetriNetwork.Lib.Transitions.ConflictResolvers;

namespace PetriNetwork.Lib.Network;

public class Network
{
    public List<Transition> Transitions { get; }
    public List<Position<object>> Positions { get; }
    public List<Arc<object>> Arcs { get; }
    private IConflictResolver _conflictResolver;
    private double _currTime = 0;
    private double _nextTime = double.MaxValue;

    public Network(
        List<Transition> transitions, List<Position<object>> positions, 
        List<Arc<object>> arcs, IConflictResolver conflictResolver)
    {
        Transitions = transitions;
        Positions = positions;
        Arcs = arcs;
        _conflictResolver = conflictResolver;
        Check();
    }

    public void Check()
    {
        foreach (var transition in Transitions)
        {
            transition.Check();
        }
        
         // check if all positions have at least 1 arc ???
    }
    
    public void Simulate(double time)
    {
        StepIn();
        _currTime = _nextTime;
        while (_currTime < time)
        {
            Step();
        }
    }

    private void Step()
    {
        StepOut();
        StepIn();
        _currTime = _nextTime;
    }

    private void StepIn()
    {
        var activeTransitions = GetActiveTransitions();
        _nextTime = double.MaxValue;
        while (activeTransitions.Count > 0)
        {
            var transition = _conflictResolver.ResolveConflict(activeTransitions);
            transition.CurrTime = _currTime;
            transition.StartTransition();
            _nextTime = Math.Min(_nextTime, transition.NextEventTime);
            activeTransitions = GetActiveTransitions();
        }
    }

    private List<Transition> GetActiveTransitions()
    {
        List<Transition> activeTransitions = new List<Transition>();
        foreach (var transition in Transitions)
        {
            if (transition.IsConditionsDone())
            {
                activeTransitions.Add(transition);
            }
        }

        return activeTransitions;
    }

    private void StepOut()
    {
        foreach (var transition in Transitions)
        {
            if (transition.NextEventTime == _currTime)
            {
                transition.EndTransition();
                transition.CurrTime = _currTime;
            }
        }
    }
}