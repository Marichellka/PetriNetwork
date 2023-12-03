namespace PetriNetwork.Lib.Transitions.Processors;

public abstract class Processor
{
    public abstract PriorityQueue<IEnumerable<object>, double> ProcessingItems { get; }
    public abstract double NextEventTime { get; }
    public abstract double CurrTime { get; set; }

    public virtual double Mean
    {
        get => _updateCount == 0 ? 0 : _mean / _updateCount;
    }

    private int _updateCount;
    private double _mean;
    public abstract void Process(IEnumerable<object> markers, double delay);
    public abstract IEnumerable<object> EndProcess();
    public void UpdateMean()
    {
        _updateCount++;
        _mean += ProcessingItems.Count;
    }
}