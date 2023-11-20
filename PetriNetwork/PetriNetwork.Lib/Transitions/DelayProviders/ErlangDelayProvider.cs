using MathNet.Numerics.Distributions;

namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class ErlangDelayProvider: IDelayProvider
{
    private Erlang _erlang;

    public ErlangDelayProvider(double mean, double variance)
    {
        int shape = (int)((mean * mean) / variance);
        double rate = shape/mean;
        _erlang = new Erlang(shape, rate);
    }

    public double GetDelay(IEnumerable<object> items)
    {
        return _erlang.Sample();
    }
}