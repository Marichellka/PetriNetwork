namespace PetriNetwork.Lib.Transitions.ConflictResolvers;

public interface IConflictResolver
{
    public abstract Transition ResolveConflict(List<Transition> conflictTransitions);
}