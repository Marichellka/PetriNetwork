using MathNet.Numerics.Distributions;

namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class ErlangDelayProvider: IDelayProvider
{
    private Erlang _erlang;

    public ErlangDelayProvider(double meanDelay, int shape)
    {
        _erlang = new Erlang(shape, shape / meanDelay);
    }

    public double GetDelay(IEnumerable<object> items)

    {
        return _erlang.Sample();
    }
}