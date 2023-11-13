namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class ExponentialDelayProvider: IDelayProvider
{
    private double _meanDelay;
    public ExponentialDelayProvider(double meanDelay)
    {
        _meanDelay = meanDelay;
    }
    
    public double GetDelay(IEnumerable<object> items)
    {
        double delay = Random.Shared.NextDouble();
        while (delay == 0)
        {
            delay = Random.Shared.NextDouble();
        }
        
        delay = -_meanDelay * Math.Log(delay);
        return delay;
    }
}