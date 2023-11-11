namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class ConstantDelayProvider<T>: IDelayProvider<T>
{
    private double _delay;

    public ConstantDelayProvider(double delay)
    {
        _delay = delay;
    }

    public double GetDelay(T item)
    {
        return _delay;
    }
}