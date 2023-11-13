namespace PetriNetwork.Lib.Transitions.DelayProviders;

public interface IDelayProvider
{
    public double GetDelay(IEnumerable<object> items);
}