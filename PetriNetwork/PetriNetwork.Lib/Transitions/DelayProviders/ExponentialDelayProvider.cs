namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class ExponentialDelayProvider<T>: IDelayProvider<T>
{
    private double _meanDelay;
    public ExponentialDelayProvider(double meanDelay)
    {
        _meanDelay = meanDelay;
    }
    
    public double GetDelay(T item)
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