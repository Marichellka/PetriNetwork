using PetriNetwork.Lib.Markers;

namespace PetriNetwork.Lib.Transitions.DelayProviders;

public class NodeRepairDelayProvider: IDelayProvider
{
    public double GetDelay(IEnumerable<object> items)
    {
        Node node = (Node)items.Single(x => x.GetType() == typeof(Node));
        return node.RepairTime;
    }
}