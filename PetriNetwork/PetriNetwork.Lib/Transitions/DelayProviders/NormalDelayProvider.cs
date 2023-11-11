namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class NormalDelayProvider<T>: IDelayProvider<T>
{
    private double _delayMean;
    private double _delayDeviation;

    public NormalDelayProvider(double delayMean, double delayDeviation)
    {
        _delayMean = delayMean;
        _delayDeviation = delayDeviation;
    }

    public double GetDelay(T item)
    {
        double delay;
        double u1 = Random.Shared.NextDouble(); 
        double u2 = Random.Shared.NextDouble();
        double randomNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                               Math.Sin(2.0 * Math.PI * u2);
        
        delay = _delayMean + _delayDeviation * randomNormal;
        return delay;
    }
}