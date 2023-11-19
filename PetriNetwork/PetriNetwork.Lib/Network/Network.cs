using PetriNetwork.Lib.Arcs;
using PetriNetwork.Lib.Positions;
using PetriNetwork.Lib.Transitions;
using PetriNetwork.Lib.Transitions.ConflictResolvers;

namespace PetriNetwork.Lib.Network;

public class Network
{
    public List<Transition> Transitions { get; }
    public List<Position> Positions { get; }
    public List<Arc> Arcs { get; }
    private IConflictResolver _conflictResolver;
    private double _currTime = 0;
    private double _nextTime = double.MaxValue;

    public Network(
        List<Transition> transitions, List<Position> positions, 
        List<Arc> arcs, IConflictResolver conflictResolver)
    {
        Transitions = transitions;
        Positions = positions;
        Arcs = arcs;
        _conflictResolver = conflictResolver;
        Check();
    }

    private void Check()
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
        UpdateCurrTime();
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
            var transition = activeTransitions[0];
            var conflictFinder = new ConflictFinder();
            conflictFinder.TryFindConflict(transition);
            if (conflictFinder.Transitions.Count > 1) 
                transition = _conflictResolver.ResolveConflict(
                    conflictFinder.Transitions.ToList(), conflictFinder.Positions.ToList());
            
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
            if (transition.IsReady())
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
            while (Math.Abs(transition.NextEventTime - _currTime) < 0.0001)
            {
                transition.EndTransition();
                transition.CurrTime = _currTime;
            }
        }
    }

    private void UpdateCurrTime()
    {
        foreach (var transition in Transitions)
        {
            transition.CurrTime = _currTime;
        }

        foreach (var position in Positions)
        {
            position.CurrTime = _currTime;
        }
    }
}