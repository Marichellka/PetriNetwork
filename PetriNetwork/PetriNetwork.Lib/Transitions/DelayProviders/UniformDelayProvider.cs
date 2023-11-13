namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class UniformDelayProvider: IDelayProvider
{
    private double _minDelay;
    private double _maxDelay;

    public UniformDelayProvider(double maxDelay, double minDelay)
    {
        _maxDelay = maxDelay;
        _minDelay = minDelay;
    }

    public double GetDelay(IEnumerable<object> items)
    {
        double delay = Random.Shared.NextDouble();
        while (delay == 0)
        {
            delay = Random.Shared.NextDouble();
        }
        
        delay = _minDelay + delay * (_maxDelay - _minDelay);
        return delay;
    }
}