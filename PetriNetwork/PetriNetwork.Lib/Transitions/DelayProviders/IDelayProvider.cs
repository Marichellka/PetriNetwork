namespace PetriNetwork.Lib.Transitions.DelayProviders;

public interface IDelayProvider<T>
{
    public double GetDelay(T item);
}