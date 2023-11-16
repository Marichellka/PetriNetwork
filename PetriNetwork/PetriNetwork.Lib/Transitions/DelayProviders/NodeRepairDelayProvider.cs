using PetriNetwork.Lib.Markers;

namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class NodeRepairDelayProvider: IDelayProvider
{
    public double GetDelay(IEnumerable<object> items)
    {
        double delay =- 1;
        foreach (var item in items)
        {
            if (item is Node node)
            {
                if (Math.Abs(delay - (-1)) > 0.0001)
                    throw new ArgumentException("Provided more than 1 Node");

                delay = node.RepairTime;
            }
                
        }

        return delay;
    }
}