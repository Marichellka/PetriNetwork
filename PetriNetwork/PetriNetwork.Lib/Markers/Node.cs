namespace PetriNetwork.Lib.Markers;

public class Node
{
    private double _prevTimeUpdate = 0;
    public double TimeInSystem { get; set; }
    public double RepairTime { get; set; }
    public bool PartiallyProcessed { get; set; }
    
    public void Update(double currTime)
    {
        TimeInSystem += currTime - _prevTimeUpdate;
    }
}