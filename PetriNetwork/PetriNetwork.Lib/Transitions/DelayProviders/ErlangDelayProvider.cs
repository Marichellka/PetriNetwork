using MathNet.Numerics.Distributions;

namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class ErlangDelayProvider<T>: IDelayProvider<T>
{
    private Erlang _erlang;

    public ErlangDelayProvider(double meanDelay, int shape)
    {
        _erlang = new Erlang(shape, shape / meanDelay);
    }

    public double GetDelay(T item)
    {
        return _erlang.Sample();
    }
}