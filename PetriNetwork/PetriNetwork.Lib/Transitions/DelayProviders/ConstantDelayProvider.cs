namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class ConstantDelayProvider: IDelayProvider
{
    private double _delay;

    public ConstantDelayProvider(double delay)
    {
        _delay = delay;
    }

    public double GetDelay(IEnumerable<object> items)
    {
        return _delay;
    }
}